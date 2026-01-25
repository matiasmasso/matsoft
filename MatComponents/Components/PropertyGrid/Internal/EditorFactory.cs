using MatComponents.Components.PropertyGrid.Internal.Metadata;
using MatComponents.Components.PropertyGrid.Internal;
using MatComponents.Components.PropertyGrid.Internal.Editors.Builtin;
using MatComponents.Components.PropertyGrid.Internal.Editors.Custom;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Reflection;

namespace MatComponents.Components.PropertyGrid.Internal;

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
            var editorType = Resolve(meta);

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
    // RESOLUCIÓ D’EDITOR PER METADATA
    // ------------------------------------------------------------

    public static Type Resolve(PgPropertyMetadata meta)
    {
        // 1. Custom editor attribute overrides everything
        if (meta.CustomEditor != null)
            return meta.CustomEditor;

        // 2. Search-based lookup (large datasets)
        if (meta.SearchFunc != null)
            return typeof(SearchLookupEditor<>).MakeGenericType(meta.PropertyType);

        // 3. Preloaded lookup values (small datasets)
        if (meta.LookupValues != null)
            return typeof(LookupEditor<>).MakeGenericType(meta.PropertyType);

        // 4. Enum
        if (meta.PropertyType.IsEnum)
            return typeof(EnumEditor);

        // 5. Primitive types
        if (meta.PropertyType == typeof(string)) return typeof(StringEditor);
        if (meta.PropertyType == typeof(int)) return typeof(IntEditor);
        if (meta.PropertyType == typeof(double)) return typeof(DoubleEditor);
        if (meta.PropertyType == typeof(decimal)) return typeof(DecimalEditor);
        if (meta.PropertyType == typeof(bool)) return typeof(BoolEditor);
        if (meta.PropertyType == typeof(DateTime)) return typeof(DateEditor);

        // 6. Complex type → nested object editor
        if (meta.IsComplexType)
            return typeof(ObjectEditor);

        // 7. Fallback
        return typeof(StringEditor);
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