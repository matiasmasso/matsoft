

Public Class Frm_Contact_Email

    Private mEmail As Email
    Private mBlocked As Boolean
    Private mAllowEvents As Boolean
    Private mLastValidatedObject As Object
    Private mDirtySsc As Boolean = False
    Private mDirtyContacts As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Ico
        Clx
        Obsoleto
    End Enum

    Public Sub New(ByVal oEmail As Email)
        MyBase.New()
        Me.InitializeComponent()
        mEmail = oEmail
        LoadBadMails()
        LoadSscs()
        Refresca()
    End Sub

    Private Sub Refresca()
        With mEmail
            TextBoxEmail.Text = .Adr
            TextBoxContactNom.Text = .Nom
            Xl_Rol1.Rol = .Rol
            TextBoxPwd.Text = .Pwd
            TextBoxGuid.Text = .Guid.ToString
            TextBoxGuid.Enabled = False
            ComboBoxLang.Text = .Lang.Tag
            mBlocked = Not .UsrAllowed
            Select Case BLL.BLLSession.Current.User.Rol.Id
                Case Rol.Ids.SuperUser
                    TextBoxGuid.Visible = True
                    TextBoxGuid.Enabled = True
                    TextBoxGuid.ReadOnly = True
                    LabelGuid.Visible = True
                    Dim oContextMenu As New ContextMenuStrip
                    oContextMenu.Items.Add(New ToolStripMenuItem("copiar", My.Resources.Copy, AddressOf Do_CopyGuid))
                    TextBoxGuid.ContextMenuStrip = oContextMenu
            End Select

            If .BadMail = -1 Or .BadMail = MaxiSrvr.Email.BadMailErrs.None Then
            Else
                CheckBoxBadMail.Checked = True
                ComboBoxBadMail.Visible = True
                ComboBoxBadMail.SelectedValue = .BadMail
            End If

            CheckBoxPrivat.Checked = (.xPrivat = MaxiSrvr.TriState.Verdadero)
            CheckBoxObsoleto.Checked = (.xObsoleto = MaxiSrvr.TriState.Verdadero)

            Dim oPropietari As New DTOUser(mEmail.Guid)
            oPropietari.Rol = mEmail.Rol
            If Not BLL.BLLUser.IsAllowedToRead(BLL.BLLSession.Current.User, oPropietari) Then
                TextBoxPwd.ReadOnly = True

            End If

            'If Not root.Usuari.AllowContactBrowse(mEmail) Then
            'End If

            If .ProductRange.Count > 0 Then
                CheckBoxProductRange.Checked = True
                Xl_ProductRange.Visible = True
                Xl_ProductRange.Product = .ProductRange(0)
            End If

            If .HasSocialNetworkUsers Then
                Xl_SocialNetworkUsers1.Email = mEmail
            Else
                TabControl1.TabPages.Remove(TabPageSocialNetworks)
            End If
        End With

        PictureBoxGravatar.Image = Gravatar.Image(TextBoxEmail.Text)

        Xl_Contacts1.Contacts = mEmail.Contacts
        mAllowEvents = True
    End Sub

    Private Sub Do_CopyGuid(sender As Object, e As System.EventArgs)
        Clipboard.SetDataObject(mEmail.Guid.ToString, True)
    End Sub

    Private Sub LoadSscs()

        Dim oSubscripcions As List(Of DTOSubscription) = BLL.BLLSubscriptions.All(mEmail.Rol)
        Dim oItem As MaxiSrvr.ListItem
        For Each oRolSsc As DTOSubscription In oSubscripcions
            oItem = New MaxiSrvr.ListItem(oRolSsc.Id, BLL.BLLSubscription.Nom(oRolSsc, BLL.BLLApp.Lang))
            Dim idx As Integer = CheckedListBoxSsc.Items.Add(oItem)
            For Each oEmailSsc As DTOSubscription In mEmail.Subscripcions
                If oEmailSsc.Id = oRolSsc.Id Then
                    CheckedListBoxSsc.SetItemChecked(idx, True)
                    Exit For
                End If
            Next
        Next
    End Sub

 
    Private Sub LoadBadMails()
        If ComboBoxBadMail.Items.Count > 0 Then Exit Sub
        Dim SQL As String = "SELECT ID,NOM FROM BADMAIL WHERE ID>0 ORDER BY ID"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxBadMail
            .DataSource = oDs.Tables(0)
            .DisplayMember = "NOM"
            .ValueMember = "ID"
        End With
    End Sub

    Private Sub EnableButtons()
        If mAllowEvents Then
            If mBlocked Then
                ButtonOk.Enabled = False
            Else
                If maxisrvr.IsValidEmailAddress(TextBoxEmail.Text) Then
                    ButtonOk.Enabled = True
                Else
                    ButtonOk.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_Rol1.Rol.Id = Rol.Ids.NotSet Then
            MsgBox("falta rol!", MsgBoxStyle.Exclamation, "MAT.NET")
        End If

        With mEmail
            .Adr = TextBoxEmail.Text.ToLower
            .Nom = TextBoxContactNom.Text
            .Lang = New DTOLang(ComboBoxLang.Text)
            .Rol = Xl_Rol1.Rol
            .Pwd = TextBoxPwd.Text
            If CheckBoxBadMail.Checked Then
                .BadMail = ComboBoxBadMail.SelectedValue
            Else
                .BadMail = MaxiSrvr.Email.BadMailErrs.None
            End If
            .xPrivat = IIf(CheckBoxPrivat.Checked, MaxiSrvr.TriState.Verdadero, MaxiSrvr.TriState.Falso)
            .xObsoleto = IIf(CheckBoxObsoleto.Checked, MaxiSrvr.TriState.Verdadero, MaxiSrvr.TriState.Falso)

            .ProductRange = New Products
            If CheckBoxProductRange.Checked Then
                If Xl_ProductRange.Product IsNot Nothing Then
                    .ProductRange.Add(Xl_ProductRange.Product)
                End If
            End If

            If mDirtyContacts Then
                .Contacts = Xl_Contacts1.Contacts
            End If

        End With

        Dim exs as New List(Of exception)
        If mEmail.Update( exs) Then
            If mDirtySsc Then
                Dim oSscs As New List(Of DTOSubscription)
                For Each oItem As MaxiSrvr.ListItem In CheckedListBoxSsc.CheckedItems
                    oSscs.Add(New DTOSubscription(oItem.Value))
                Next

                Dim oUser As New DTOUser(mEmail.Guid)
                exs = New List(Of exception)
                If Not BLL.BLLSubscriptions.Update(oUser, oSscs, exs) Then
                    MsgBox(BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
            RaiseEvent AfterUpdate(mEmail, e)
            Me.Close()
        Else
            MsgBox("error al grabar correu:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
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
     TextBoxContactNom.TextChanged, _
      TextBoxPwd.TextChanged, _
       TextBoxGuid.TextChanged, _
        Xl_Rol1.Changed, _
         ComboBoxBadMail.SelectedValueChanged, _
          CheckBoxBadMail.CheckedChanged, _
 _
            Xl_ProductRange.AfterUpdate, _
 _
              CheckBoxPrivat.CheckedChanged, _
 _
                CheckBoxObsoleto.CheckedChanged

        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub



    Private Sub TextBoxEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxEmail.TextChanged
        Dim ColorOk As System.Drawing.Color = Color.FromArgb(230, 255, 255)
        Dim ColorFailed As System.Drawing.Color = Color.LightYellow
        Dim BlOk As Boolean = maxisrvr.IsValidEmailAddress(TextBoxEmail.Text)
        TextBoxEmail.BackColor = IIf(BlOk, ColorOk, ColorFailed)
        If mAllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub TextBoxEmail_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxEmail.Validated
        If mEmail.Adr <> TextBoxEmail.Text.Trim() Then
            Dim oEmail As Email = MaxiSrvr.EmailLoader.FromAddress(TextBoxEmail.Text)
            If oEmail IsNot Nothing Then
                mEmail.MergeWith(oEmail)
                mDirtyContacts = True
                Refresca()
            End If
        End If
    End Sub

    Private Sub TextBoxPwd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxPwd.DoubleClick
        Dim BlAllow As Boolean = mEmail.UsrAllowed

        If BlAllow Then
            MsgBox(TextBoxPwd.Text, MsgBoxStyle.Information, "PASSWORD")
        Else
            MsgBox("No está autoritzat per aquesta operació", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub ToolStripButtonPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start("IExplore.exe", mEmail.UrlPro())
    End Sub


    Private Sub ToolStripButtonMatPocket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sUrl As String = "http://www.matiasmasso.es/matpocket?Id=" & mEmail.Guid.ToString
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub ToolStripButtonSend_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewMail(mEmail.Adr)
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        If mEmail.Delete( exs) Then
            RaiseEvent AfterUpdate(sender, e)
            Me.Close()
        Else
            MsgBox("error al eliminar " & mEmail.Adr & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub ComboBoxLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxLang.SelectedIndexChanged
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


    Private Sub Xl_Contacts1_RequestToDelete(sender As Object, e As EventArgs) Handles Xl_Contacts1.RequestToDelete
        mDirtyContacts = True
        EnableButtons()
    End Sub
End Class