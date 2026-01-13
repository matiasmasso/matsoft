

Imports LegacyHelper

Public Class Frm_Contact_Email
    Private _User As DTOUser
    Private mBlocked As Boolean
    Private mAllowEvents As Boolean
    Private mLastValidatedObject As Object
    Private mDirtySsc As Boolean = False
    Private mDirtyContacts As Boolean = False

    Private _BadMailsLoaded As Boolean
    Private _LogsLoaded As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        General
        Logs
    End Enum

    Private Enum Cols
        Id
        Ico
        Clx
        Obsoleto
    End Enum

    Public Sub New(ByVal oUser As DTOUser)
        MyBase.New()
        Me.InitializeComponent()
        _User = oUser
    End Sub

    Private Async Sub Frm_Contact_Email_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.User.Load(exs, _User) Then
            Await LoadBadMails()
            Await LoadSscs()
            Await Refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function Refresca() As Task
        With _User
            If TextBoxEmail.Text = "" Then TextBoxEmail.Text = .EmailAddress
            If TextBoxNickname.Text = "" Then TextBoxNickname.Text = .NickName
            If TextBoxObs.Text = "" Then TextBoxObs.Text = .Obs
            Xl_LookupRol1.Rol = .Rol
            TextBoxPwd.Text = .Password
            TextBoxGuid.Text = .Guid.ToString
            TextBoxGuid.Enabled = False
            If .Lang IsNot Nothing Then
                ComboBoxLang.Text = .Lang.Tag
            End If
            mBlocked = Not FEB.User.IsAllowedToRead(Current.Session.User, _User)
            Select Case Current.Session.User.Rol.id
                Case DTORol.Ids.superUser
                    TextBoxGuid.Visible = True
                    TextBoxGuid.Enabled = True
                    TextBoxGuid.ReadOnly = True
                    LabelGuid.Visible = True
                    Dim oContextMenu As New ContextMenuStrip
                    oContextMenu.Items.Add(New ToolStripMenuItem("copiar", My.Resources.Copy, AddressOf Do_CopyGuid))
                    TextBoxGuid.ContextMenuStrip = oContextMenu
            End Select

            If .BadMail IsNot Nothing Then
                CheckBoxBadMail.Checked = True
                ComboBoxBadMail.Visible = True
                For Each cod As DTOGuidNom In ComboBoxBadMail.DataSource
                    If cod.Guid.Equals(.BadMail.Guid) Then
                        ComboBoxBadMail.SelectedItem = cod
                    End If
                Next
            End If

            CheckBoxPrivat.Checked = .Privat
            CheckBoxObsoleto.Checked = .Obsoleto
            CheckBoxNoNews.Checked = .NoNews

            If Not FEB.User.IsAllowedToRead(Current.Session.User, _User) Then
                TextBoxPwd.ReadOnly = True
            End If

            'If Not root.Usuari.AllowContactBrowse(mEmail) Then
            'End If

            'If .ProductRange.Count > 0 Then
            'CheckBoxProductRange.Checked = True
            'Xl_ProductRange.Visible = True
            'Xl_ProductRange.Product = .ProductRange(0)
            'End If

        End With

        Dim exs As New List(Of Exception)
        Dim url = GravatarHelper.Url(TextBoxEmail.Text)
        Dim oImgBytes = Await FEB.FetchImage(exs, url)
        PictureBoxGravatar.Image = LegacyHelper.ImageHelper.FromBytes(oImgBytes)

        Xl_Contacts1.Load(_User.Contacts)
        mAllowEvents = True
    End Function

    Private Sub Do_CopyGuid(sender As Object, e As System.EventArgs)
        Clipboard.SetDataObject(_User.Guid.ToString, True)
    End Sub

    Private Async Function LoadSscs() As Task
        Dim exs As New List(Of Exception)
        Dim oRolSscs = Await FEB.Subscriptions.All(exs, Current.Session.Emp, _User.Rol)
        Dim oUserSscs = Await FEB.Subscriptions.All(exs, Current.Session.Emp, _User)
        If exs.Count = 0 Then
            Dim oItem As ListItem
            For Each oRolSsc As DTOSubscription In oRolSscs
                oItem = New ListItem(0, oRolSsc.Nom.Tradueix(DTOApp.Current.Lang))
                oItem.Tag = oRolSsc
                Dim idx As Integer = CheckedListBoxSsc.Items.Add(oItem)
                If oUserSscs.Any(Function(x) x.Equals(oRolSsc)) Then
                    CheckedListBoxSsc.SetItemChecked(idx, True)
                End If
            Next
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Function LoadBadMails() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.User.BadMailReasons(exs, Current.Session.Lang)
        If exs.Count = 0 Then
            If Not _BadMailsLoaded Then
                With ComboBoxBadMail
                    .DataSource = items
                    .DisplayMember = "Nom"
                    .ValueMember = "Guid"
                End With
                _BadMailsLoaded = True
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub EnableButtons()
        If mAllowEvents Then
            If mBlocked Then
                ButtonOk.Enabled = False
            Else
                If DTOUser.CheckEmailSintaxis(TextBoxEmail.Text) Then
                    ButtonOk.Enabled = True
                Else
                    ButtonOk.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_LookupRol1.Rol Is Nothing OrElse Xl_LookupRol1.Rol.id = DTORol.Ids.notSet Then
            MsgBox("falta rol!", MsgBoxStyle.Exclamation, "MAT.NET")
        Else

            With _User
                .EmailAddress = TextBoxEmail.Text.ToLower
                .NickName = TextBoxNickname.Text
                .Obs = TextBoxObs.Text
                .Lang = DTOLang.Factory(ComboBoxLang.Text)
                .Rol = Xl_LookupRol1.Rol
                .Password = TextBoxPwd.Text
                If CheckBoxBadMail.Checked Then
                    .BadMail = New DTOCod(ComboBoxBadMail.SelectedItem.Guid)
                Else
                    .BadMail = Nothing
                End If
                .Privat = CheckBoxPrivat.Checked
                .Obsoleto = CheckBoxObsoleto.Checked
                .NoNews = CheckBoxNoNews.Checked

                '.ProductRange = New Products
                'If CheckBoxProductRange.Checked Then
                ' If Xl_ProductRange.Product IsNot Nothing Then
                ' .ProductRange.Add(Xl_ProductRange.Product)
                ' End If
                ' End If

                If mDirtyContacts Then
                    .Contacts = Xl_Contacts1.Values
                End If

            End With

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.User.Update(exs, _User) Then
                If mDirtyContacts Then
                    Await FEB.User.UpdateContacts(exs, _User)
                End If

                If mDirtySsc Then
                    Dim oSscs As New List(Of DTOSubscription)
                    For Each oItem As ListItem In CheckedListBoxSsc.CheckedItems
                        oSscs.Add(oItem.Tag)
                    Next

                    exs = New List(Of Exception)
                    If Not Await FEB.Subscriptions.Update(exs, _User, oSscs) Then
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                End If
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al grabar correu:")
            End If
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBoxBadMail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBadMail.CheckedChanged
        If mAllowEvents Then
            ComboBoxBadMail.Visible = CheckBoxBadMail.Checked
            EnableButtons()
        End If
    End Sub

    Private Sub CheckBoxProductRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxProductRange.CheckedChanged
        If mAllowEvents Then
            Xl_ProductRange.Visible = CheckBoxProductRange.Checked
            EnableButtons()
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxNickname.TextChanged,
      TextBoxPwd.TextChanged,
       TextBoxGuid.TextChanged,
         ComboBoxBadMail.SelectedValueChanged,
          CheckBoxBadMail.CheckedChanged,
            Xl_ProductRange.AfterUpdate,
              CheckBoxPrivat.CheckedChanged,
                CheckBoxObsoleto.CheckedChanged,
                 CheckBoxNoNews.CheckedChanged

        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub



    Private Sub TextBoxEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxEmail.TextChanged
        Dim ColorOk As System.Drawing.Color = Color.FromArgb(230, 255, 255)
        Dim ColorFailed As System.Drawing.Color = Color.LightYellow
        Dim BlOk As Boolean = DTOUser.CheckEmailSintaxis(TextBoxEmail.Text)
        TextBoxEmail.BackColor = IIf(BlOk, ColorOk, ColorFailed)
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Async Sub TextBoxEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxEmail.Validated
        Dim exs As New List(Of Exception)
        Dim sText As String = TextBoxEmail.Text.Trim
        If _User.EmailAddress <> sText Or sText.IndexOf("<") >= 0 Then
            If sText.IndexOf("<") >= 0 And sText.IndexOf(">") >= 0 Then
                Dim iPos As Integer = sText.IndexOf("<")
                TextBoxNickname.Text = sText.Substring(0, iPos).Trim
                TextBoxEmail.Text = sText.Substring(iPos + 1, sText.Length - iPos - 2)
                Application.DoEvents()
            End If


            Dim oSourceUser = Await FEB.User.FromEmail(exs, Current.Session.Emp, TextBoxEmail.Text.Trim)
            If exs.Count = 0 Then
                If oSourceUser IsNot Nothing Then
                    DTOUser.Merge(_User, oSourceUser)
                    mDirtyContacts = True
                    Await Refresca()
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub TextBoxPwd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxPwd.DoubleClick
        Dim BlAllow As Boolean = FEB.User.IsAllowedToRead(Current.Session.User, _User)

        If BlAllow Then
            MsgBox(TextBoxPwd.Text, MsgBoxStyle.Information, "Password")
        Else
            MsgBox("No está autoritzat per aquesta operació", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


    Private Sub ComboBoxLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        ComboBoxLang.SelectedIndexChanged,
         Xl_LookupRol1.AfterUpdate

        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub CheckedListBoxSsc_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxSsc.ItemCheck
        If mAllowEvents Then
            mDirtySsc = True
            EnableButtons()
        End If
    End Sub



    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
            Case Tabs.Logs
                If Not _LogsLoaded Then
                    Dim items = Await FEB.User.WebLogs(exs, _User)
                    If exs.Count = 0 Then
                        Xl_WebLogs21.Load(items, Xl_WebLogs2.Modes.CodsPerUser)
                        _LogsLoaded = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select

    End Sub

    Private Sub Xl_Contacts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.RequestToRefresh
        mDirtyContacts = True
        EnableButtons()
    End Sub

    Private Sub Xl_Contacts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.RequestToAddNew
        Dim oFrm As New Frm_ContactSearch
        AddHandler oFrm.itemSelected, AddressOf OnAddNewSelected
        oFrm.Show()
    End Sub

    Private Async Sub OnAddNewSelected(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = e.Argument
        Dim items = Xl_Contacts1.Values.ToList()
        items.Add(oContact)
        Dim sortedItems = items.OrderBy(Function(x) x.FullNom).ToList()
        Await Xl_Contacts1.Load(sortedItems)
        mDirtyContacts = True
        EnableButtons()
    End Sub

    Private Sub Xl_Contacts1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.AfterUpdate
        mDirtyContacts = True
        EnableButtons()
    End Sub
End Class