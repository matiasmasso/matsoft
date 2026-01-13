Imports System.Diagnostics

Public Class VivaceController
    Inherits _BaseController

    '<HttpGet>
    '<Route("api/vivace/outvivace/pruebas")>
    'Public Function PruebasGet() As String
    'Dim retval As DTOValidationResult = BLLOutVivace.Procesa(value, pruebas:=True)
    'Dim retval As New DTOValidationResult
    'Return retval.ToString
    'End Function

    <HttpPost>
    <Route("api/vivace/outvivace/pruebas")>
    Public Function Pruebas(value As DTOOutVivaceLog) As DTOValidationResult
        Dim retval As DTOValidationResult = BLLOutVivace.Procesa(value, pruebas:=True)
        Return retval
    End Function

    <HttpPost>
    <Route("api/vivace/outvivace")>
    Public Function Produccion(value As DTOOutVivaceLog) As DTOValidationResult
        'Public Function Produccion(value As DTOOutVivaceLog) As DTOValidationResult
        Dim retval As DTOValidationResult = BLLOutVivace.Procesa(value, pruebas:=False)
        Return retval
    End Function

End Class

Public Class DTOTest
    Property nom As String
End Class
