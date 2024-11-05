using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.World.Component;

public class UserInputComponent : EntityComponent
{
    // Static fields.
    public static readonly NamespacedKey KEY = NamespacedKey.Default("user_input");


    // Fields.
    public bool IsInputRegistered { get; set; }
    public IEnumerable<InputAction> CurrentInputActions { get; set; }
    public IEnumerable<InputAction> PreviousInputActions { get; set; }


    // Private fields.
    private readonly HashSet<InputAction> _currentInputActions = new();
    private readonly HashSet<InputAction> _previousInputActions = new();


    // Constructors.
    public UserInputComponent() : base(KEY) { }


    // Methods.
    public void UpdateInputActions(IEnumerable<InputAction> inputActions)
    {
        ArgumentNullException.ThrowIfNull(inputActions, nameof(inputActions));

        _previousInputActions.Clear();
        foreach (InputAction Action in _currentInputActions)
        {
            _currentInputActions.Add(Action);
        }

        _currentInputActions.Clear();
        foreach (InputAction Action in inputActions)
        {
            _currentInputActions.Add(Action);
        }
    }

    public void ClearInputActions()
    {
        _currentInputActions.Clear();
        _previousInputActions.Clear();
    }

    public bool IsActionActive(InputAction action)
    {
        return _currentInputActions.Contains(action);
    }

    public bool WasActionActive(InputAction action)
    {
        return _previousInputActions.Contains(action);
    }

    public bool IsActionJustNowActive(InputAction action)
    {
        return IsActionActive(action) && !WasActionActive(action);
    }

    public bool IsActionJustNowInactive(InputAction action)
    {
        return !IsActionJustNowActive(action) && WasActionActive(action);
    }


    // Inherited methods.,
    public override EntityComponent CreateCopy()
    {
        UserInputComponent CreatedComponent = new()
        {
            IsInputRegistered = IsInputRegistered
        };

        CreatedComponent.UpdateInputActions(_previousInputActions);
        CreatedComponent.UpdateInputActions(_currentInputActions);

        return CreatedComponent;
    }
}