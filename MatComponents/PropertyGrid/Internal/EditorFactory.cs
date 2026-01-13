using System;
using System.Reflection;
using MatComponents.PropertyGrid.Internal.Editors.Builtin;
using MatComponents.PropertyGrid.Internal.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace MatComponents.PropertyGrid.Internal;

public static class EditorFactory
{
    public static RenderFragment Render(
        PgPropertyMetadata meta,
        object model,
        FieldIdentifier field)
        => builder =>
        {
            var prop = meta.Property;
            var value = prop.GetValue(model);
            var type = prop.PropertyType;

            // 1) Editor personalitzat via atribut
            if (meta.CustomEditor != null)
            {
                BuildCustomEditor(builder, meta.CustomEditor, prop, model, value, field, type);
                return;
            }

            // 2) Editor per opcions (dropdown)
            var optionsAttr = prop.GetCustomAttribute<PgOptionsAttribute>();
            if (optionsAttr != null)
            {
                BuildOptionsEditor(builder, prop, model, value, field, optionsAttr.Items);
                return;
            }

            // 3) Editor per tipus
            var editorType = ResolveEditorType(type);

            // 4) Editor d’objectes (nested)
            if (editorType == typeof(ObjectEditor))
            {
                BuildObjectEditor(builder, meta, prop, model, value, field);
                return;
            }

            // 5) Editor builtin
            BuildBuiltinEditor(builder, editorType, prop, model, value, field, type);
        };

    // ------------------------------------------------------------
    // RESOLUCIÓ D’EDITOR PER TIPUS
    // ------------------------------------------------------------
    private static Type ResolveEditorType(Type type)
    {
        if (type == typeof(string)) return typeof(StringEditor);
        if (type == typeof(int)) return typeof(IntEditor);
        if (type == typeof(double)) return typeof(DoubleEditor);
        if (type == typeof(decimal)) return typeof(DecimalEditor);
        if (type == typeof(bool)) return typeof(BoolEditor);
        if (type == typeof(DateTime)) return typeof(DateEditor);
        if (type.IsEnum) return typeof(EnumEditor);

        return typeof(ObjectEditor);
    }

    // ------------------------------------------------------------
    // EDITOR BUILTIN
    // ------------------------------------------------------------
    private static void BuildBuiltinEditor(
        RenderTreeBuilder builder,
        Type editorType,
        PropertyInfo prop,
        object model,
        object value,
        FieldIdentifier field,
        Type type)
    {
        builder.OpenComponent(0, editorType);
        builder.AddAttribute(1, "Value", value);

        var callback = CreateTypedCallback(prop, model);
        builder.AddAttribute(2, "ValueChanged", callback);

        builder.AddAttribute(3, "Field", field);

        // Editor d’enums → passar EnumType
        if (type.IsEnum)
            builder.AddAttribute(4, "EnumType", type);

        builder.CloseComponent();
    }

    // ------------------------------------------------------------
    // EDITOR PER OPCIONS (PgOptionsAttribute)
    // ------------------------------------------------------------
    private static void BuildOptionsEditor(
        RenderTreeBuilder builder,
        PropertyInfo prop,
        object model,
        object value,
        FieldIdentifier field,
        string[] items)
    {
        builder.OpenComponent(0, typeof(DropdownEditor));
        builder.AddAttribute(1, "Value", value?.ToString());

        var callback = EventCallback.Factory.Create<string>(model, v => prop.SetValue(model, v));
        builder.AddAttribute(2, "ValueChanged", callback);

        builder.AddAttribute(3, "Field", field);
        builder.AddAttribute(4, "Items", items);

        builder.CloseComponent();
    }

    // ------------------------------------------------------------
    // EDITOR PERSONALITZAT (PgEditorAttribute)
    // ------------------------------------------------------------
    private static void BuildCustomEditor(
        RenderTreeBuilder builder,
        Type editorType,
        PropertyInfo prop,
        object model,
        object value,
        FieldIdentifier field,
        Type type)
    {
        builder.OpenComponent(0, editorType);
        builder.AddAttribute(1, "Value", value);

        var callback = EventCallback.Factory.Create<object>(model, v => prop.SetValue(model, v));
        builder.AddAttribute(2, "ValueChanged", callback);

        builder.AddAttribute(3, "Field", field);

        if (type.IsEnum)
            builder.AddAttribute(4, "EnumType", type);

        builder.CloseComponent();
    }

    // ------------------------------------------------------------
    // EDITOR D’OBJECTES (nested)
    // ------------------------------------------------------------
    private static void BuildObjectEditor(
        RenderTreeBuilder builder,
        PgPropertyMetadata meta,
        PropertyInfo prop,
        object model,
        object value,
        FieldIdentifier field)
    {
        // Crear instància si és null
        if (value == null && prop.PropertyType.GetConstructor(Type.EmptyTypes) != null)
        {
            value = Activator.CreateInstance(prop.PropertyType);
            prop.SetValue(model, value);
        }

        var node = new ObjectNode
        {
            Label = meta.Label,
            Instance = value,
            Properties = ReflectionHelpers.GetMetadata(value)
        };

        builder.OpenComponent(0, typeof(ObjectEditor));
        builder.AddAttribute(1, "Node", node);
        builder.CloseComponent();
    }

    // ------------------------------------------------------------
    // CALLBACK TIPAT
    // ------------------------------------------------------------
    private static object CreateTypedCallback(PropertyInfo prop, object model)
    {
        var type = prop.PropertyType;

        if (type == typeof(string))
            return EventCallback.Factory.Create<string>(model, v => prop.SetValue(model, v));

        if (type == typeof(int))
            return EventCallback.Factory.Create<int>(model, v => prop.SetValue(model, v));

        if (type == typeof(bool))
            return EventCallback.Factory.Create<bool>(model, v => prop.SetValue(model, v));

        if (type == typeof(double))
            return EventCallback.Factory.Create<double>(model, v => prop.SetValue(model, v));

        if (type == typeof(decimal))
            return EventCallback.Factory.Create<decimal>(model, v => prop.SetValue(model, v));

        if (type == typeof(DateTime))
            return EventCallback.Factory.Create<DateTime>(model, v => prop.SetValue(model, v));

        if (type.IsEnum)
            return EventCallback.Factory.Create<string>(model, v =>
            {
                var parsed = Enum.Parse(type, v);
                prop.SetValue(model, parsed);
            });

        return EventCallback.Factory.Create<object>(model, v => prop.SetValue(model, v));
    }
}