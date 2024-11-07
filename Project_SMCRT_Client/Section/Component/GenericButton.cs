
using GHEngine;
using GHEngine.Assets;
using GHEngine.Audio.Source;
using GHEngine.Frame;
using GHEngine.Frame.Animation;
using GHEngine.Frame.Item;
using GHEngine.GameFont;
using GHEngine.IO;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Client.Section.Component;

public class GenericButton : IRenderableItem, ITimeUpdatable
{
    // Static fields.
    public static readonly FloatColor DefaultColor = new FloatColor(1f, 1f, 1f, 1f);
    public static readonly FloatColor ColorGood = new FloatColor(0.2f, 1f, 0.2f, 1f);
    public static readonly FloatColor ColorBad = new FloatColor(1f, 0.2f, 0.2f, 1f);
    public static readonly FloatColor ColorGenericBlue = new FloatColor(0.286f, 0.717f, 0.949f, 1f);
    public static readonly FloatColor ColorGenericPurple = new FloatColor(0.705f, 0.286f, 0.949f, 1f);
    public static readonly FloatColor ColorGenericPink = new FloatColor(0.949f, 0.286f, 0.639f, 1f);


    // Fields.
    public bool IsVisible { get; set; }
    public bool IsFunctional { get; set; }
    public float Scale { get; set; } = 1f;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string? Text
    {
        get => _text;
        set => _text = value;
    }

    public Action? LeftClickAction { get; set; }
    public Action? RightClickAction { get; set; }
    public FloatColor ButtonColor { get; set; } = DefaultColor;


    // Private fields.
    private readonly GHFontFamily? _textFont;
    private readonly SpriteItem? _buttonSprite;
    private readonly ISound? _highlightSound;
    private readonly ISound[]? _clickSounds;
    private readonly Vector2 _size = new(0.35f, 0.125f);
    private string? _text;
    private readonly GameServices _services;
    private bool _wasMouseInBounds = false;


    // Constructors.
    public GenericButton(string? buttonText,
        GHFontFamily? textFont,
        IAnimationInstance? buttonAnimation,
        ISound? highlightSound,
        ISound[]? clickSounds,
        GameServices services)
    {
        _text = buttonText;
        _textFont = textFont;
        _buttonSprite = buttonAnimation == null ? null : new(buttonAnimation);
        _highlightSound = highlightSound;
        _clickSounds = clickSounds?.ToArray();

        if (_buttonSprite != null)
        {
            _buttonSprite.Origin = new(0.5f, 0.5f);
            _buttonSprite.IsSizeAdjusted = true;
            _buttonSprite.IsPositionAdjusted = false;
        }
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }


    // Private methods.
    private Vector2 GetButtonSize()
    {
        return GHMath.GetWindowAdjustedVector(_size * Scale, _services.UserInput.InputAreaRatio);
    }

    private bool IsMouseInBounds()
    {
        Vector2 SpriteSize = GetButtonSize();
        Vector2 AllowedBoundsStart = Position - (SpriteSize / 2);
        Vector2 AllowedBoundsEnd = Position + (SpriteSize / 2);

        Vector2 MousePos = _services.UserInput.VirtualMousePositionCurrent;

        return (AllowedBoundsStart.X <= MousePos.X) && (MousePos.X <= AllowedBoundsEnd.X)
            && (AllowedBoundsStart.Y <= MousePos.Y) && (MousePos.Y <= AllowedBoundsEnd.Y);
    }

    private void PlayClickSound()
    {
        if (_clickSounds != null)
        {
            IPreSampledSoundInstance Sound = (IPreSampledSoundInstance)_clickSounds[Random.Shared.Next(_clickSounds.Length)].CreateInstance();
            Sound.Sampler.SampleSpeed = 0.8f + Random.Shared.NextDouble() * 0.4f;
            Sound.Sampler.Volume = 0.4f;
            _services.AudioEngine.ScheduleAction(() => _services.AudioEngine.AddSoundInstance(Sound));
        }
    }


    // Inherited methods.
    public void Render(IRenderer renderer, IProgramTime time)
    {
        if (_buttonSprite != null)
        {
            _buttonSprite.Position = Position;
            _buttonSprite.Size = _size * Scale;
            _buttonSprite.Render(renderer, time);
        }

        if ((_text != null) && (_textFont != null))
        {
            Vector2 ButtonSize = GetButtonSize();
            Vector2 StartPosition = Position - (ButtonSize * 0.4f);
            FontRenderProperties RenderProperties = new(_textFont, false, false, 0f, 0f);
            renderer.DrawString(RenderProperties,
                _text,
                StartPosition,
                Color.White,
                0f,
                Vector2.Zero,
                ButtonSize * new Vector2(0.8f, 1.8f),
                null,
                null,
                null);
        }
    }

    public void Update(IProgramTime time)
    {
        bool MouseInBounds = IsMouseInBounds();

        if (MouseInBounds && _services.UserInput.WereMouseButtonsJustPressed(MouseButton.Left))
        {
            LeftClickAction?.Invoke();
            PlayClickSound();
        }
        if (MouseInBounds && _services.UserInput.WereMouseButtonsJustPressed(MouseButton.Right))
        {
            RightClickAction?.Invoke();
            PlayClickSound();
        }
        if ((MouseInBounds && !_wasMouseInBounds) && (_highlightSound != null))
        {
            _services.AudioEngine.ScheduleAction(() => _services.AudioEngine.AddSoundInstance(_highlightSound.CreateInstance()));
        }

        if (_buttonSprite != null)
        {
            _buttonSprite.Update(time);
            _buttonSprite.Mask = (Color)(ButtonColor * (MouseInBounds ? 1.6f : 1f));
        }

        _wasMouseInBounds = MouseInBounds;
    }
}
