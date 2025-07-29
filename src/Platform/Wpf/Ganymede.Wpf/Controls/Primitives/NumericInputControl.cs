using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Windows.Controls.Primitives;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;
using Exp = System.Linq.Expressions.Expression;

namespace TheXDS.Ganymede.Controls.Primitives;

/// <summary>
/// Base class for all input controls for numeric values.
/// </summary>
/// <typeparam name="T">
/// Numeric type to implement the input control for.
/// </typeparam>
public abstract class NumericInputControl<T> : FormInputControl where T : unmanaged, IComparable<T>, IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IConvertible
{
    private delegate bool TryParseCallback(string input, out T value);

    private record Settings(Func<T, T, T> Add, Func<T, T, T> Subtract, Func<T, T> MathAbs, TryParseCallback TryParse, T One, T MinValue, T MaxValue)
    {
        public Settings() : this(
            GetAddMethod(),
            GetSubtractMethod(),
            GetAbsMethod(),
            GetTryParseMethod(),
            GetNumber("1"),
            (T)typeof(T).GetField("MinValue")!.GetValue(null)!,
            (T)typeof(T).GetField("MaxValue")!.GetValue(null)!)
        {
        }

        private static Func<T, T> GetAbsMethod()
        {
            return typeof(Math).GetMethod("Abs", BindingFlags.Static | BindingFlags.Public, [typeof(T)]) is { } abs && abs.GetParameters()[0].ParameterType == typeof(T)
                ? (Func<T, T>)Delegate.CreateDelegate(typeof(Func<T, T>), abs)
                : x => x;
        }

        private static Func<T, T, T> GetAddMethod()
        {
            return GetOperatorMethod(Exp.Add)
                ?? ((a, b) => UncheckedNumericConvert(a.ToInt32(null) + b.ToInt32(null)));

        }

        private static Func<T, T, T> GetSubtractMethod()
        {
            return GetOperatorMethod(Exp.Subtract)
                ?? ((a, b) => UncheckedNumericConvert(a.ToInt32(null) - b.ToInt32(null)));
        }

        private static Func<T, T, T>? GetOperatorMethod(Func<Exp, Exp, System.Linq.Expressions.BinaryExpression> op)
        {
            if (typeof(T).ImplementsOperator(op))
            {
                var p1 = Exp.Parameter(typeof(T));
                var p2 = Exp.Parameter(typeof(T));
                return Exp.Lambda<Func<T, T, T>>(op(p1, p2), p1, p2).Compile();
            }
            return null;
        }

        private static TryParseCallback GetTryParseMethod()
        {
            return (TryParseCallback)Delegate.CreateDelegate(typeof(TryParseCallback), typeof(T).GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, [typeof(string), typeof(T).MakeByRefType()])!);
        }

        private static T GetNumber(string input)
        {
            return (T)typeof(T).GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, [typeof(string)])!.Invoke(null, [input])!;
        }

        private static T UncheckedNumericConvert<TSource>(TSource value)
        {
            var param = Exp.Parameter(typeof(TSource), nameof(value));
            var body = Exp.Convert(param, typeof(T));
            var lambda = Exp.Lambda(body, param).Compile();
            return (T)lambda.DynamicInvoke(value)!;
        }
    }

    /// <summary>
    /// Identifies the <see cref="Maximum"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MaximumProperty;

    /// <summary>
    /// Identifies the <see cref="Minimum"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty MinimumProperty;

    /// <summary>
    /// Identifies the <see cref="Step"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StepProperty;

    /// <summary>
    /// Identifies the <see cref="Value"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty;

    /// <summary>
    /// Identifies the <see cref="UpDownButtonsVisibility"/> dependency
    /// property.
    /// </summary>
    public static readonly DependencyProperty UpDownButtonsVisibilityProperty;

    private static readonly Settings _Settings = new();

    static NumericInputControl()
    {
        MaximumProperty = NewDp2Way<T, NumericInputControl<T>>(nameof(Maximum), _Settings.MaxValue, OnRangeChanged, OnMaximumCoerce);
        MinimumProperty = NewDp2Way<T, NumericInputControl<T>>(nameof(Minimum), _Settings.MinValue, OnRangeChanged, OnMinimumCoerce);
        StepProperty = NewDp2Way<T, NumericInputControl<T>>(nameof(Step), _Settings.One, coerceValue: OnStepCoerce, validate: OnStepValidate);
        ValueProperty = NewDp2Way<T, NumericInputControl<T>>(nameof(Value), default, OnValueChanged, OnValueCoerce);
        UpDownButtonsVisibilityProperty = NewDp<Visibility, NumericInputControl<T>>(nameof(UpDownButtonsVisibility), Visibility.Visible);
    }

    private static object OnStepCoerce(DependencyObject d, object baseValue)
    {
        var c = (NumericInputControl<T>)d;
        return ((T)baseValue).Clamp(_Settings.One, _Settings.MathAbs(_Settings.Add(c.Minimum, c.Maximum)));
    }

    private static bool OnStepValidate(object value)
    {
        return value is T v && v.CompareTo(default) > 0;
    }

    private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(ValueProperty);
    }

    private static object OnMinimumCoerce(DependencyObject d, object baseValue)
    {
        var c = (NumericInputControl<T>)d;
        return ((T)baseValue).Clamp(_Settings.MinValue, c.Maximum);
    }

    private static object OnMaximumCoerce(DependencyObject d, object baseValue)
    {
        var c = (NumericInputControl<T>)d;
        return ((T)baseValue).Clamp(c.Minimum, _Settings.MaxValue);
    }

    private static object OnValueCoerce(DependencyObject d, object baseValue)
    {
        var c = (NumericInputControl<T>)d;
        return ((T)baseValue).Clamp(c.Minimum, c.Maximum);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var c = (NumericInputControl<T>)d;
        if (c._tb is not null)
        {
            c._tb.Text = e.NewValue.ToString();
        }
    }

    private TextBox? _tb;

    /// <summary>
    /// Gets or sets the maximum allowed value on this control.
    /// </summary>
    public T Maximum
    {
        get => (T)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    /// <summary>
    /// Gets or sets the minimum allowed value on this control.
    /// </summary>
    public T Minimum
    {
        get => (T)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired step increase/decrease whenever the up/down
    /// buttons are used.
    /// </summary>
    public T Step
    {
        get => (T)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    /// <summary>
    /// Gets or sets the current control value.
    /// </summary>
    public T Value
    {
        get => (T)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates the desired visibility for the
    /// Up/Down arrow buttons.
    /// </summary>
    public Visibility UpDownButtonsVisibility
    {
        get => (Visibility)GetValue(UpDownButtonsVisibilityProperty);
        set => SetValue(UpDownButtonsVisibilityProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _tb = Template.FindName("PART_input", this) as TextBox;
        if (_tb is not null)
        {
            _tb.Text = Value.ToString();
            _tb.TextChanged += Tb_TextChanged;
            _tb.GotKeyboardFocus += (_, _) => _tb.SelectAll();
        }
        WireUp("increase", _Settings.Add);
        WireUp("decrease", _Settings.Subtract);
    }

    private void WireUp(string part, Func<T, T, T> op)
    {
        if (Template.FindName($"PART_{part}", this) is RepeatButton btn)
        {
            btn.Click += (_, _) => Value = op(Value, Step).Clamp(Minimum, Maximum);
        }
    }

    private void Tb_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Value.ToString() != _tb!.Text)
        {
            if (_Settings.TryParse(_tb.Text, out var v))
            {
                Value = v;
            }
            else
            {
                _tb.Text = Value.ToString();
                _tb.SelectAll();
            }
        }
    }
}