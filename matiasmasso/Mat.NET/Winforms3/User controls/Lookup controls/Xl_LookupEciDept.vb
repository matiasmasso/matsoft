Public Class Xl_LookupEciDept
    Inherits Xl_LookupTextboxButton

    Private _EciDept As DTO.Integracions.ElCorteIngles.Dept

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property EciDept() As DTO.Integracions.ElCorteIngles.Dept
        Get
            Return _EciDept
        End Get
        Set(ByVal value As DTO.Integracions.ElCorteIngles.Dept)
            _EciDept = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.EciDept = Nothing
    End Sub

    Private Sub Xl_LookupEciDept_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_EciDept))
    End Sub

    Private Sub onEciDeptSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _EciDept = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _EciDept Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = String.Format("{0} {1}", _EciDept.Id, _EciDept.Nom)
            'Dim oMenu_EciDept As New Menu_EciDept(_EciDept)
            'AddHandler oMenu_EciDept.AfterUpdate, AddressOf refresca
            'MyBase.SetContextMenuRange(oMenu_EciDept.Range)
        End If
    End Sub


End Class

