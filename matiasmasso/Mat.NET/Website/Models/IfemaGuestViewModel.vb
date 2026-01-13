Imports System.ComponentModel.DataAnnotations

Public Class IfemaGuestViewModel
    Property EmailAddress As String

    <Required(ErrorMessage:="Falta completar el nombre")> _
    <MaxLength(50, ErrorMessage:="El nombre no puede exceder de 50 caracteres")> _
    Property Nom As String

    <Required(ErrorMessage:="Falta completar los apellidos")> _
    <MaxLength(50, ErrorMessage:="Los apellidos no pueden exceder de 50 caracteres")> _
    Property Cognoms As String

    <Required(ErrorMessage:="Falta completar el cargo")> _
    <MaxLength(50, ErrorMessage:="El cargo no puede exceder de 50 caracteres")> _
    Property Cargo As String

    <Required(ErrorMessage:="Falta completar el nif")> _
    <MaxLength(9, ErrorMessage:="El nif no puede exceder de 9 caracteres")> _
    Property Nif As String

    <Required(ErrorMessage:="Falta completar el nombre de la empresa")> _
    <MaxLength(50, ErrorMessage:="El nombre de la empresa no puede exceder de 50 caracteres")> _
    Property Empresa As String

    <Required(ErrorMessage:="Falta seleccionar su actividad principal")> _
    Property Actividad As Integer

    <Required(ErrorMessage:="Falta rellenar su dirección")> _
    <MaxLength(50, ErrorMessage:="La dirección no puede exceder de 50 caracteres")> _
    Property Direccion As String

    <Required(ErrorMessage:="Falta rellenar su código postal")> _
    <MaxLength(9, ErrorMessage:="El código postal no puede exceder de 9 caracteres")> _
    Property Zip As String

    <Required(ErrorMessage:="Falta indicar su población")> _
    <MaxLength(50, ErrorMessage:="La población no puede exceder de 50 caracteres")> _
    Property Poblacion As String

    <Required(AllowEmptyStrings:=False, ErrorMessage:="No hay ningún pais seleccionado")> _
    Property CountryCod As String

    <Required(ErrorMessage:="Falta indicar un teléfono")> _
    <MaxLength(20, ErrorMessage:="El número de teléfono no puede exceder de 20 caracteres")> _
    Property tel As String

    <Required(ErrorMessage:="Falta indicar un número de móvil")> _
    <MaxLength(20, ErrorMessage:="El número de móvil no puede exceder de 20 caracteres")> _
    Property movil As String

    <MaxLength(20, ErrorMessage:="El número de fax no puede exceder de 20 caracteres")> _
    Property Fax As String


End Class
