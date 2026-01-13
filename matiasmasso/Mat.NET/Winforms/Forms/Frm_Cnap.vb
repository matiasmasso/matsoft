

Public Class Frm_Cnap
    Private _Cnap As DTOCnap = Nothing

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        nomCurt
        nomLlarg
        Skus
    End Enum

    Public Sub New(ByVal oCnap As DTOCnap)
        MyBase.New()
        Me.InitializeComponent()
        _Cnap = oCnap
    End Sub

    Private Sub Frm_Cnap_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Cnap.Load(_Cnap, exs) Then
            With _Cnap
                refrescaParent()
                TextBoxCod.Text = .Id
                Xl_LangsTextShort.Load(.NomShort)
                Xl_LangsTextLong.Load(.NomLong)
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Cnap
            .Id = TextBoxCod.Text
            .NomShort = Xl_LangsTextShort.Value
            .NomLong = Xl_LangsTextLong.Value
            .Tags = TextHelper.StringListFromMultiline(TextBoxTags.Text)
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Cnap.Update(_Cnap, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cnap))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el codi")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxCod.TextChanged,
     Xl_LangsTextShort.AfterUpdate,
      Xl_LangsTextLong.AfterUpdate,
       TextBoxTags.TextChanged

        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _Cnap.FullNom(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Cnap.Delete(_Cnap, exs) Then
                RaiseEvent AfterDelete(_Cnap, EventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el codi")
            End If
        End If
    End Sub

    Private Sub TextBoxParent_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxParent.DoubleClick
        Dim oFrm As New Frm_Cnap(_Cnap.Parent)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaParent
        oFrm.Show()
    End Sub

    Private Sub refrescaParent()
        If _Cnap.Parent IsNot Nothing Then
            TextBoxParent.Text = _Cnap.Parent.FullNom(Current.Session.Lang)
            Dim oMenu_Cnap As New Menu_Cnap(_Cnap.Parent)
            AddHandler oMenu_Cnap.AfterUpdate, AddressOf refrescaParent

            Dim oContextMenu As New ContextMenuStrip
            oContextMenu.Items.AddRange(oMenu_Cnap.Range)
            TextBoxParent.ContextMenuStrip = oContextMenu
        End If
    End Sub

    Private Async Function RefrescaProducts() As Task
        Dim exs As New List(Of Exception)
        Dim oProducts As New List(Of DTOProduct)
        oProducts.AddRange(Await FEB2.ProductSkus.All(exs, _Cnap))
        If exs.Count = 0 Then
            Xl_Products1.Load(oProducts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControlLong_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControlLong.SelectedIndexChanged
        Select Case TabControlLong.SelectedIndex
            Case Tabs.Skus
                Await RefrescaProducts()
        End Select
    End Sub

    Private Async Sub Xl_Products1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Products1.AfterUpdate
        Await RefrescaProducts()
    End Sub

End Class