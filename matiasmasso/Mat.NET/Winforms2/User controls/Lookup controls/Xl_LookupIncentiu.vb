Public Class Xl_LookupIncentiu
    Inherits Xl_LookupTextboxButton

    Private _Incentiu As DTOIncentiu

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New()
        MyBase.New
        MyBase.BackGroundColorWhenNotNull = Color.LightGreen
    End Sub

    Public Shadows Property Incentiu() As DTOIncentiu
        Get
            Return _Incentiu
        End Get
        Set(ByVal value As DTOIncentiu)
            _Incentiu = value
            SetContextMenu()
            If _Incentiu Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Incentiu.Title.Tradueix(Current.Session.Lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Incentiu = Nothing
    End Sub

    Private Sub Xl_LookupIncentiu_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Incentius(Nothing, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onIncentiuSelected
        oFrm.Show()
    End Sub

    Private Sub onIncentiuSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Incentiu = e.Argument
        SetContextMenu()
        MyBase.Text = Format(_Incentiu.FchFrom, "dd/MM/yy") & "-" & _Incentiu.Title.Tradueix(Current.Session.Lang)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Incentiu IsNot Nothing Then
            Dim oMenu_Incentiu As New Menu_Incentiu(_Incentiu)
            AddHandler oMenu_Incentiu.AfterUpdate, AddressOf onIncentiuSelected
            oContextMenu.Items.AddRange(oMenu_Incentiu.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


End Class
