Public Class Frm_Condicio

    Private _Condicio As DTOCondicio
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Esp
        Cat
        Eng
        Por
    End Enum

    Public Sub New(value As DTOCondicio)
        MyBase.New()
        Me.InitializeComponent()
        _Condicio = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Condicio.Load(_Condicio, exs) Then
            With _Condicio
                TextBoxTitleEsp.Text = .Title.Esp
                TextBoxTextEsp.Text = .Excerpt.Esp
                TextBoxTitleCat.Text = .Title.Cat
                TextBoxTextCat.Text = .Excerpt.Cat
                TextBoxTitleEng.Text = .Title.Eng
                TextBoxTextEng.Text = .Excerpt.Eng
                TextBoxTitlePor.Text = .Title.Por
                TextBoxTextPor.Text = .Excerpt.Por
                Xl_CondicioCapitols1.Load(.Capitols, Current.Session.Lang)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitleEsp.TextChanged,
         TextBoxTextEsp.TextChanged,
        TextBoxTitleCat.TextChanged,
         TextBoxTextCat.TextChanged,
        TextBoxTitleEng.TextChanged,
         TextBoxTextEng.TextChanged,
        TextBoxTitlePor.TextChanged,
         TextBoxTextPor.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Condicio
            .Title.esp = TextBoxTitleEsp.Text
            .Excerpt.esp = TextBoxTextEsp.Text
            .Title.cat = TextBoxTitleCat.Text
            .Excerpt.cat = TextBoxTextCat.Text
            .Title.eng = TextBoxTitleEng.Text
            .Excerpt.eng = TextBoxTextEng.Text
            .Title.por = TextBoxTitlePor.Text
            .Excerpt.por = TextBoxTextPor.Text
            .Capitols = Xl_CondicioCapitols1.Values
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Condicio.Update(_Condicio, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Condicio))
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
            If Await FEB2.Condicio.Delete(_Condicio, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Condicio))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_CondicioCapitols1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CondicioCapitols1.RequestToRefresh
        refrescaCapitols()
    End Sub

    Private Sub Xl_CondicioCapitols1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CondicioCapitols1.RequestToAddNew
        Dim item As New DTOCondicio.Capitol
        With item
            .Parent = _Condicio
            .Ord = Xl_CondicioCapitols1.Values.Count + 1
            .UsrLog.UsrLastEdited = Current.Session.User.ToGuidNom
        End With

        Dim oFrm As New Frm_CondicioCapitol(item)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaCapitols
    End Sub

    Private Async Sub refrescaCapitols()
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.CondicioCapitols.Headers(_Condicio, exs)
        Xl_CondicioCapitols1.Load(items, CurrentLang)
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If _AllowEvents Then
            Xl_CondicioCapitols1.Lang = CurrentLang()
        End If
    End Sub

    Private Function CurrentLang() As DTOLang
        Dim retval As DTOLang = Nothing
        Select Case TabControl1.SelectedIndex
            Case Tabs.Cat
                retval = DTOLang.CAT
            Case Tabs.Eng
                retval = DTOLang.ENG
            Case Tabs.Por
                retval = DTOLang.POR
            Case Else
                retval = DTOLang.ESP
        End Select
        Return retval
    End Function

End Class

