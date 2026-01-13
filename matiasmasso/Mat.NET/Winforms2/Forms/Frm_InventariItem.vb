Public Class Frm_InventariItem
    Private _InventariItem As DTOImmoble.InventariItem
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Downloads
    End Enum

    Public Sub New(value As DTOImmoble.InventariItem)
        MyBase.New()
        Me.InitializeComponent()
        _InventariItem = value
    End Sub

    Private Sub Frm_InventariItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.InventariItem.Load(exs, _InventariItem) Then
            With _InventariItem
                TextBoxNom.Text = .Nom
                TextBoxObs.Text = .Obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxObs.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _InventariItem
            .Nom = TextBoxNom.Text
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.InventariItem.Update(exs, _InventariItem) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_InventariItem))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.InventariItem.Delete(exs, _InventariItem) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_InventariItem))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Downloads
                Static LoadedDocFileSrcs As Boolean
                If Not LoadedDocFileSrcs Then
                    refrescaDocFileSrcs()
                End If
        End Select
    End Sub

    Private Async Sub refrescaDocFileSrcs()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB.DocFileSrcs.All(_InventariItem, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToRefresh
        refrescaDocFileSrcs()
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.download
            .Src = _InventariItem
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocFileSrcs
        oFrm.Show()
    End Sub
End Class


