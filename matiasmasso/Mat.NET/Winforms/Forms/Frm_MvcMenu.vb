Public Class Frm_MvcMenu

    Private _MvcMenu As DTOMenu
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOMenu)
        MyBase.New()
        Me.InitializeComponent()
        _MvcMenu = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.MvcMenu.Load(_MvcMenu, exs) Then
            With _MvcMenu
                UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOMenu.Cods), , .Cod)
                TextBoxNomEsp.Text = .Caption.Esp
                TextBoxNomCat.Text = .Caption.Cat
                TextBoxNomEng.Text = .Caption.Eng
                TextBoxUrl.Text = .Url
                Xl_Rols1.Load(.Rols)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged, _
         TextBoxNomCat.TextChanged, _
          TextBoxNomEng.TextChanged, _
           ComboBoxCod.SelectedIndexChanged, _
            TextBoxUrl.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _MvcMenu
            .Cod = ComboBoxCod.SelectedValue
            .Caption.SetText(DTOLang.ESP, TextBoxNomEsp.Text)
            .Caption.SetText(DTOLang.CAT, TextBoxNomCat.Text)
            .Caption.SetText(DTOLang.ENG, TextBoxNomEng.Text)
            '.Caption.SetText(DTOLang.POR, TextBoxNomPor.Text)
            .Url = TextBoxUrl.Text
            .Rols = Xl_Rols1.SelectedItems
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.MvcMenu.Update(_MvcMenu, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_MvcMenu))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
            If Await FEB2.MvcMenu.Delete(_MvcMenu, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_MvcMenu))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_Rols1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Rols1.RequestToAddNew
        Dim oFrm As New Frm_Rols(oSelectionMode:=DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onAddingRols
        oFrm.Show()
    End Sub

    Private Sub onAddingRols(sender As Object, e As MatEventArgs)
        'Xl_Rols1.AddRols(e.Argument)
        'ButtonOk.Enabled = True
    End Sub
End Class


