Public Class Xl_LookupPgcPlan
    Inherits Xl_LookupTextboxButton

    Private _PgcPlan As DTOPgcPlan

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PgcPlan() As DTOPgcPlan
        Get
            Return _PgcPlan
        End Get
        Set(ByVal value As DTOPgcPlan)
            _PgcPlan = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PgcPlan = Nothing
    End Sub

    Private Sub Xl_LookupPgcPlan_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _PgcPlan IsNot Nothing Then
            If Not FEB.PgcPlan.Load(_PgcPlan, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        Dim oFrm As New Frm_PgcPlans(_PgcPlan, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onPgcPlanSelected
        oFrm.Show()
    End Sub

    Private Sub onPgcPlanSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PgcPlan = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PgcPlan Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _PgcPlan.Nom
            Dim oMenu_PgcPlan As New Menu_PgcPlan(_PgcPlan)
            AddHandler oMenu_PgcPlan.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PgcPlan.Range)
        End If
    End Sub

End Class
