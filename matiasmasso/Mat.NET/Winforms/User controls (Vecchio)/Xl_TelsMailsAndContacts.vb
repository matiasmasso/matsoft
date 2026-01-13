

Public Class Xl_TelsMailsAndContacts
    'Implements IUpdatableDetailsPanel

    Private mContact As Contact
    Private mTb As DataTable
    Private mArrayList As ArrayList

    Private mDirty As Boolean
    Private mDirtyTels As Boolean
    Private mDirtyEmails As Boolean
    Private mDirtySubContacts As Boolean

    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) ' Implements IUpdatableDetailsPanel.AfterUpdate

    Private Enum Cols
        Ico
        Text
    End Enum

    Private Enum Cods
        NotSet
        Tel
        Email
        Contact
    End Enum


    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            mContact = value
            mArrayList = New ArrayList
            For Each oTel As Tel In mContact.Tels
                mArrayList.Add(oTel)
            Next
            For Each oEmail As Email In mContact.Emails
                mArrayList.Add(oEmail)
            Next
            For Each oSubContact As String In mContact.SubContacts
                mArrayList.Add(oSubContact)
            Next
            If mArrayList.Count > 0 Then
                LabelTelIntroInfo.Visible = False
            End If
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        mTb = CreateTable()
        Dim oRow As DataRow = Nothing
        For Each itm As Object In mArrayList
            oRow = mTb.NewRow
            LoadRow(oRow, itm)
            mTb.Rows.Add(oRow)
        Next

        mAllowEvents = False
        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = mTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Ico)
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        If DataGridView1.CurrentRow IsNot Nothing Then
            Dim idx As Integer = DataGridView1.CurrentRow.Index
            Dim oObj As Object = mArrayList(idx)
            Select Case GetCodFromObject(oObj)
                Case Cods.Tel
                    oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
                    oContextMenu.Items.Add(oMenuItem)
                Case Cods.Email
                    oMenuItem = New ToolStripMenuItem("e-mail")
                    oContextMenu.Items.Add(oMenuItem)
                    Dim oEmail As Email = CType(oObj, Email)
                    Dim oMenuMail As New Menu_Email(oEmail)
                    oMenuItem.DropDownItems.AddRange(oMenuMail.Range)
                    AddHandler oMenuMail.AfterUpdate, AddressOf refreshRequest
                Case Cods.Contact
                    oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
                    oContextMenu.Items.Add(oMenuItem)
            End Select
        End If


        oMenuItem = New ToolStripMenuItem("afegir")
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("telefon", Nothing, AddressOf AddNewTel))
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("fax", Nothing, AddressOf AddNewFax))
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("movil", Nothing, AddressOf AddNewMovil))
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("email", Nothing, AddressOf AddNewEmail))
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("contacte", Nothing, AddressOf AddNewSubContact))
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("eliminar", Nothing, AddressOf Remove)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable("TELS")
        With oTb.Columns
            .Add("ICO", System.Type.GetType("System.Byte[]"))
            .Add("TXT", System.Type.GetType("System.String"))
        End With
        Return oTb
    End Function

    Private Sub LoadRow(ByRef oRow As DataRow, ByVal itm As Object)
        Select Case GetCodFromObject(itm)
            Case Cods.Tel
                LoadRowFromTel(oRow, itm)
            Case Cods.Email
                LoadRowFromEmail(oRow, itm)
            Case Cods.Contact
                LoadRowFromSubContact(oRow, itm)
        End Select
    End Sub

    Private Sub LoadRowFromTel(ByRef oRow As DataRow, ByVal oTel As Tel)
        Dim oIcon As Image = Nothing
        Select Case oTel.Cod
            Case Tel.Cods.tel
                If oTel.Privat Then
                    oIcon = My.Resources.tel_red
                Else
                    oIcon = My.Resources.tel
                End If
            Case Tel.Cods.fax
                oIcon = My.Resources.fax
            Case Tel.Cods.movil
                If oTel.Privat Then
                    oIcon = My.Resources.movil_red
                Else
                    oIcon = My.Resources.movil
                End If
            Case Else
                oIcon = My.Resources.empty
        End Select

        oRow(Cols.Ico) = maxisrvr.GetByteArrayFromImg(oIcon)
        oRow(Cols.Text) = oTel.formatted & " " & oTel.Obs

    End Sub

    Private Sub LoadRowFromEmail(ByRef oRow As DataRow, ByVal oEmail As Email)
        Dim oIcon As Image = Nothing
        If oEmail.xObsoleto = MaxiSrvr.TriState.Verdadero Then
            oIcon = My.Resources.del
        ElseIf oEmail.BadMail <> Email.BadMailErrs.None Then
            oIcon = My.Resources.wrong
        ElseIf oEmail.Efras Then
            oIcon = My.Resources.MailSobreGrocBlau
        Else
            oIcon = My.Resources.MailSobreGroc
        End If

        oRow(Cols.Ico) = maxisrvr.GetByteArrayFromImg(oIcon)
        oRow(Cols.Text) = oEmail.Adr & " " & oEmail.Nom

    End Sub

    Private Sub LoadRowFromSubContact(ByRef oRow As DataRow, ByVal sSubContact As String)
        oRow(Cols.Ico) = maxisrvr.GetByteArrayFromImg(My.Resources.empty)
        oRow(Cols.Text) = sSubContact
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idx As Integer = DataGridView1.CurrentRow.Index
        Dim oObj As Object = mArrayList(idx)
        Select Case GetCodFromObject(oObj)
            Case Cods.Tel
                Dim oFrm As New Frm_Tel(oObj)
                AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
                oFrm.Show()
            Case Cods.Email
                Dim oFrm As New Frm_Contact_Email(oObj)
                AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
                oFrm.Show()
            Case Cods.Contact
                Dim oSubContact As SubContact = DirectCast(oObj, SubContact)
                Dim s As String = InputBox("persona de contacte", mContact.Clx, oSubContact.Text)
                If s = "" Then
                    Remove(sender, e)
                Else
                    oSubContact.Text = s
                    refreshRequest(oSubContact, EventArgs.Empty)
                End If
        End Select

    End Sub

    Private Sub AddNewTel(ByVal sender As Object, ByVal e As System.EventArgs)
        AddNewGralTel(Tel.Cods.tel)
    End Sub

    Private Sub AddNewFax(ByVal sender As Object, ByVal e As System.EventArgs)
        AddNewGralTel(Tel.Cods.fax)
    End Sub

    Private Sub AddNewMovil(ByVal sender As Object, ByVal e As System.EventArgs)
        AddNewGralTel(Tel.Cods.movil)
    End Sub

    Private Sub AddNewGralTel(ByVal oCod As Tel.Cods)
        Dim oTel As New Tel(mContact, oCod)
        Dim oFrm As New Frm_Tel(oTel)
        AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        oFrm.Show()
    End Sub

    Private Sub AddNewEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oEmail As New Email(mContact)
        'Dim oFrm As New Frm_Contact_Email(oEmail)
        'AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        'oFrm.Show()
    End Sub

    Private Sub AddNewSubContact(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = InputBox("persona de contacte", mContact.Clx)
        If s > "" Then
            Dim oSubContact As New SubContact(s)
            refreshRequest(oSubContact, EventArgs.Empty)
        End If
    End Sub

    Private Sub refreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataRow = Nothing
        Dim Done As Boolean = False
        Dim oCurrentObj As ISupportGuid = sender
        Dim oCurrentCod As Cods = GetCodFromObject(sender)
        Dim iSelectedIndex As Integer

        For i As Integer = 0 To mArrayList.Count - 1
            Select Case GetCodFromObject(mArrayList(i))
                Case Is = oCurrentCod
                    If oCurrentObj.Guid = CType(mArrayList(i), ISupportGuid).Guid Then
                        mArrayList(i) = sender
                        Done = True
                        Exit For
                    End If
                Case Is > oCurrentCod
                    mArrayList.Insert(i, sender)
                    Done = True
                    Exit For
            End Select
        Next

        If Not Done Then mArrayList.Add(sender)

        LabelTelIntroInfo.Visible = (mArrayList.Count = 0)


        If DataGridView1.CurrentRow IsNot Nothing Then
            iSelectedIndex = DataGridView1.CurrentRow.Index
        End If

        mTb.Rows.Clear()
        For Each itm As Object In mArrayList
            oRow = mTb.NewRow
            LoadRow(oRow, itm)
            mTb.Rows.Add(oRow)
        Next

        If DataGridView1.Rows.Count > iSelectedIndex Then
            DataGridView1.CurrentCell = DataGridView1.Rows(iSelectedIndex).Cells(Cols.Text)
        End If

        SetDirty(oCurrentCod)
    End Sub


    Private Function GetCodFromObject(ByVal oObj As Object) As Cods
        Dim oCod As Cods
        If TypeOf (oObj) Is Tel Then
            oCod = Cods.Tel
        ElseIf TypeOf (oObj) Is Email Then
            oCod = Cods.Email
        ElseIf TypeOf (oObj) Is System.String Then
            oCod = Cods.Contact
        Else
            oCod = Cods.NotSet
        End If
        Return oCod
    End Function

    Private Sub Remove(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim idx As Integer = DataGridView1.CurrentRow.Index
        Dim oCurrentCod As Cods = GetCodFromObject(mArrayList(idx))
        mArrayList.RemoveAt(idx)
        mTb.Rows.RemoveAt(idx)
        SetDirty(oCurrentCod)
    End Sub

    Private Sub SetDirty(ByVal oCod As Cods)
        Select Case oCod
            Case Cods.Tel
                mDirtyTels = True
            Case Cods.Email
                mDirtyEmails = True
            Case Cods.Contact
                mDirtySubContacts = True
        End Select

        mDirty = True
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
    End Sub

    Public Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean
        Dim retval As Boolean
        If mDirty Then
            Dim oTels As New tels
            Dim oEmails As New Emails
            Dim oSubContacts As New SubContacts
            For Each oObj As Object In mArrayList
                Select Case GetCodFromObject(oObj)
                    Case Cods.Tel
                        oTels.Add(oObj)
                    Case Cods.Email
                        oEmails.Add(oObj)
                    Case Cods.Contact
                        oSubContacts.Add(oObj)
                End Select
            Next

            If mDirtyTels Then
                ' mContact.Tel2s = oTels
                ' mContact.UpdateTel2s()
            End If

            If mDirtyEmails Then
                mContact.Emails = oEmails
                mContact.UpdateEmails( exs)
            End If

            If mDirtySubContacts Then
                mContact.SubContacts = oSubContacts
                mContact.UpdateSubContacts( exs)
            End If
            retval = True
        End If
        Return retval
    End Function

    Public Function NeededHeight() As Integer
        Dim i As Integer = DataGridView1.Rows.Count * DataGridView1.RowTemplate.Height
        Return i
    End Function
End Class
