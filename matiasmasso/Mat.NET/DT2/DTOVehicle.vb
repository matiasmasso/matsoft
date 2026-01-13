Public Class DTOVehicle
    Inherits DTOGuidNom

    Property emp As DTOEmp
    Property model As DTOVehicleModel
    Property matricula As String
    Property bastidor As String
    Property conductor As DTOContact
    Property venedor As DTOContact
    Property alta As Date
    Property baixa As Date
    Property contract As DTOContract
    Property insurance As DTOContract
    Property privat As Boolean
    <JsonIgnore> Property image As Image

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Shadows Function factory(oEmp As DTOEmp) As DTOVehicle
        Dim retval As New DTOVehicle
        With retval
            .emp = oEmp
        End With
        Return retval
    End Function

    Public Function marcaYModel() As String
        Dim sb As New Text.StringBuilder
        If _model IsNot Nothing Then
            sb.Append(_model.fullNom & " ")
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function marcaModelYMatricula() As String
        Dim retval As String = String.Format("{0} {1}", Me.marcaYModel, _matricula)
        Return retval
    End Function

    Public Function matriculaMarcaYModel() As String
        Dim retval As String = String.Format("{0} {1}", _matricula, Me.marcaYModel)
        Return retval
    End Function

    Public Function isDonatDeBaixa() As Boolean
        Return _baixa <> Date.MinValue
    End Function
End Class
