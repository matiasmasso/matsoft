Public Class Frm_WinMenuItem
    Private _WinMenuItem As DTOWinMenuItem
    Private _DirtyRols As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event AfterDelete(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOWinMenuItem)
        MyBase.New()
        Me.InitializeComponent()
        _WinMenuItem = value

        'BLL.BLLWinMenuItem.Load(_WinMenuItem)
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.WinmenuItem.Load(_WinMenuItem, exs) Then
            With _WinMenuItem
                If .Parent Is Nothing Then
                    LabelParent.Text = "(arrel)"
                Else
                    LabelParent.Text = DirectCast(.Parent, DTOWinMenuItem).LangText.Tradueix(Current.Session.Lang)
                End If
                TextBoxNomEsp.Text = .LangText.Text(DTOLang.ESP)
                TextBoxNomCat.Text = .LangText.Text(DTOLang.CAT)
                TextBoxNomEng.Text = .LangText.Text(DTOLang.ENG)
                TextBoxNomPor.Text = .LangText.Text(DTOLang.POR)
                ComboBoxCod.SelectedIndex = .Cod
                TextBoxAction.Text = .Action
                TextBoxAction.Visible = (.Cod = DTOWinMenuItem.Cods.Item)
                If Not .icon Is Nothing Then
                    Xl_ImageBig.Bitmap = LegacyHelper.ImageHelper.Converter(.icon)
                End If

                Dim allRols = Await FEB.Rols.All(exs)
                If exs.Count = 0 Then
                    Xl_RolsAllowed1.Load(allRols, .Rols)
                Else
                    UIHelper.WarnError(exs)
                End If

                Dim allEmps = Await FEB.Emps.All(exs, Current.Session.User)
                If exs.Count = 0 Then
                    Xl_Emps_Checklist1.Load(allEmps, .Emps)

                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew
                Else
                    UIHelper.WarnError(exs)
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
         Xl_RolsAllowed1.AfterUpdate,
          TextBoxNomEsp.TextChanged,
           TextBoxNomCat.TextChanged,
            TextBoxNomEng.TextChanged,
             TextBoxNomPor.TextChanged,
            Xl_ImageBig.AfterUpdate,
             TextBoxAction.TextChanged,
              Xl_Emps_Checklist1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ComboBoxCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        If _AllowEvents Then
            TextBoxAction.Visible = ComboBoxCod.SelectedIndex = DTOWinMenuItem.Cods.Item
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WinMenuItem
            .LangText = New DTOLangText(TextBoxNomEsp.Text, TextBoxNomCat.Text, TextBoxNomEng.Text, TextBoxNomPor.Text)
            .Cod = ComboBoxCod.SelectedIndex
            .Action = TextBoxAction.Text
            .icon = LegacyHelper.ImageHelper.Converter(Xl_ImageBig.Bitmap)
            .Mime = Xl_ImageBig.MimeCod
            .Rols = Xl_RolsAllowed1.CheckedValues
            .Emps = Xl_Emps_Checklist1.CheckedValues
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If _WinMenuItem.Cod = DTOWinMenuItem.Cods.NotSet Then
            MsgBox("Cal sel·leccionar un codi", MsgBoxStyle.Exclamation)
        Else
            If Await FEB.WinmenuItem.Update(_WinMenuItem, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WinMenuItem))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar")
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.WinmenuItem.Delete(_WinMenuItem, exs) Then
                RaiseEvent AfterDelete(Me, New MatEventArgs(_WinMenuItem))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


