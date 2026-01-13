

Public Class Frm_Contact_Mail
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxAtn As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents RadioButtonReceived As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSent As System.Windows.Forms.RadioButton
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents PictureBoxLocked As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewContacts As System.Windows.Forms.DataGridView
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
    Friend WithEvents CheckBoxWord As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxAtn = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxSubject = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonReceived = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSent = New System.Windows.Forms.RadioButton()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckBoxWord = New System.Windows.Forms.CheckBox()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PictureBoxLocked = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DataGridViewContacts = New System.Windows.Forms.DataGridView()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridViewContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(657, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'TextBoxAtn
        '
        Me.TextBoxAtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAtn.Location = New System.Drawing.Point(449, 214)
        Me.TextBoxAtn.Name = "TextBoxAtn"
        Me.TextBoxAtn.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxAtn.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(369, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "&a la atenció de:"
        '
        'TextBoxSubject
        '
        Me.TextBoxSubject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSubject.Location = New System.Drawing.Point(449, 238)
        Me.TextBoxSubject.Name = "TextBoxSubject"
        Me.TextBoxSubject.Size = New System.Drawing.Size(304, 20)
        Me.TextBoxSubject.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(369, 238)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "&concepte:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButtonReceived)
        Me.GroupBox1.Controls.Add(Me.RadioButtonSent)
        Me.GroupBox1.Location = New System.Drawing.Point(449, 262)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 56)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonReceived
        '
        Me.RadioButtonReceived.Location = New System.Drawing.Point(8, 32)
        Me.RadioButtonReceived.Name = "RadioButtonReceived"
        Me.RadioButtonReceived.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonReceived.TabIndex = 10
        Me.RadioButtonReceived.Text = "&rebut"
        '
        'RadioButtonSent
        '
        Me.RadioButtonSent.Location = New System.Drawing.Point(8, 12)
        Me.RadioButtonSent.Name = "RadioButtonSent"
        Me.RadioButtonSent.Size = New System.Drawing.Size(96, 16)
        Me.RadioButtonSent.TabIndex = 9
        Me.RadioButtonSent.Text = "&enviat"
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(649, 8)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(538, 8)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'CheckBoxWord
        '
        Me.CheckBoxWord.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxWord.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxWord.Checked = True
        Me.CheckBoxWord.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWord.Location = New System.Drawing.Point(625, 325)
        Me.CheckBoxWord.Name = "CheckBoxWord"
        Me.CheckBoxWord.Size = New System.Drawing.Size(128, 16)
        Me.CheckBoxWord.TabIndex = 13
        Me.CheckBoxWord.Text = "Redactar document"
        Me.CheckBoxWord.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxWord.Visible = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(12, 8)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'PictureBoxLocked
        '
        Me.PictureBoxLocked.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLocked.Image = Global.Mat.NET.My.Resources.Resources.candado
        Me.PictureBoxLocked.Location = New System.Drawing.Point(737, 194)
        Me.PictureBoxLocked.Name = "PictureBoxLocked"
        Me.PictureBoxLocked.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxLocked.TabIndex = 15
        Me.PictureBoxLocked.TabStop = False
        Me.PictureBoxLocked.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 425)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(757, 41)
        Me.Panel1.TabIndex = 17
        '
        'DataGridViewContacts
        '
        Me.DataGridViewContacts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewContacts.BackgroundColor = System.Drawing.SystemColors.MenuBar
        Me.DataGridViewContacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewContacts.Location = New System.Drawing.Point(372, 44)
        Me.DataGridViewContacts.Name = "DataGridViewContacts"
        Me.DataGridViewContacts.Size = New System.Drawing.Size(380, 132)
        Me.DataGridViewContacts.TabIndex = 19
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(3, 7)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 20
        '
        'Frm_Contact_Mail
        '
        Me.ClientSize = New System.Drawing.Size(757, 466)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.DataGridViewContacts)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBoxLocked)
        Me.Controls.Add(Me.CheckBoxWord)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBoxSubject)
        Me.Controls.Add(Me.TextBoxAtn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Name = "Frm_Contact_Mail"
        Me.Text = "CORRESPONDENCIA"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridViewContacts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mMail As Mail
    Private mLocked As Boolean
    Private mDirtyCell As Boolean
    Private mLastValidatedObject As Object
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Sub New(ByVal oMail As Mail)
        MyBase.New()
        Me.InitializeComponent()
        mMail = oMail
        With mMail
            DateTimePicker1.Value = .Fch
            TextBoxAtn.Text = .Atn
            TextBoxSubject.Text = .Subject
            RadioButtonSent.Checked = (.Cod = DTO.DTOCorrespondencia.Cods.Enviat)
            RadioButtonReceived.Checked = (.Cod = DTO.DTOCorrespondencia.Cods.Rebut)
            CheckBoxWord.Visible = RadioButtonSent.Checked
            Xl_DocFile1.Load(.DocFile)

            Dim exs As New List(Of exception)
            'mLocked = Not .AllowDelete(root.Usuari, exs)
            If .Id > 0 Then
                CheckBoxWord.Checked = False
            End If
        End With

        LoadContactGrid()
        PictureBoxLocked.Visible = mLocked
        ButtonDel.Enabled = Not mLocked
        mAllowEvents = True
    End Sub

    Private Sub LoadContactGrid()
        Dim oTb As New DataTable
        oTb.Columns.Add("Id", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("Nom", System.Type.GetType("System.String"))
        For Each oContact As Contact In mMail.Contacts
            Dim oRow As DataRow = oTb.NewRow
            oRow(Cols.Id) = oContact.Id
            oRow(Cols.Nom) = oContact.Clx
            oTb.Rows.Add(oRow)
        Next


        With DataGridViewContacts
            With .RowTemplate
                .Height = DataGridViewContacts.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "contactes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub SetDirty()
        Dim BlEnabled As Boolean = True
        If TextBoxSubject.Text = "" Then BlEnabled = False
        If Not (RadioButtonReceived.Checked Or RadioButtonSent.Checked) Then BlEnabled = False
        ButtonOk.Enabled = BlEnabled
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMail
            .Fch = DateTimePicker1.Value
            .Atn = TextBoxAtn.Text
            .Subject = TextBoxSubject.Text
            .Cod = GetCodFromRadioButtons()
            .Contacts = GetContactsFromGrid()

            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.value
            End If
        End With

        Dim exs As New List(Of exception)
        If mMail.Update(exs) Then
            If RadioButtonSent.Checked And CheckBoxWord.Checked Then
                root.ShowMailWord(mMail)
            Else
                MsgBox("registre " & mMail.Id, MsgBoxStyle.Information, "MAT.NET CORRESPONDENCIA")
            End If

            RaiseEvent AfterUpdate(mMail, New System.EventArgs)
            Me.Close()
        Else
            MsgBox("error al desar el document a correspondencia" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Function GetContactsFromGrid() As Contacts
        Dim oContacts As New Contacts
        For Each oRow As DataGridViewRow In DataGridViewContacts.Rows
            If IsNumeric(oRow.Cells(Cols.Id).Value) Then
                Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mMail.Emp, CInt(oRow.Cells(Cols.Id).Value))
                If oContact.Id > 0 Then
                    oContacts.Add(oContact)
                End If
            End If
        Next
        Return oContacts
    End Function

    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSubject.TextChanged, TextBoxAtn.TextChanged, RadioButtonReceived.CheckedChanged, RadioButtonSent.CheckedChanged, DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SetDirty()
            CheckBoxWord.Visible = RadioButtonSent.Checked
        End If
    End Sub

    Private Function GetCodFromRadioButtons() As DTO.DTOCorrespondencia.Cods
        If RadioButtonReceived.Checked Then
            Return DTO.DTOCorrespondencia.Cods.Rebut
        Else
            Return DTO.DTOCorrespondencia.Cods.Enviat
        End If
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta correspondencia?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mMail.Delete( exs) Then
                MsgBox("correspondencia " & mMail.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(mMail, New System.EventArgs)
                Me.Close()
            Else
                MsgBox("error al eliminar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub


    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridViewContacts.CellBeginEdit
        mDirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridViewContacts.CellValidating
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridViewContacts.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Nom
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oContact As Contact = Finder.FindContact(mMail.Emp, e.FormattedValue)
                        If oContact Is Nothing Then
                            e.Cancel = True
                        Else
                            mLastValidatedObject = oContact
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewContacts.CellValidated
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridViewContacts.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Nom
                    Dim oContact As Contact = CType(mLastValidatedObject, Contact)
                    oRow.Cells(Cols.Id).Value = oContact.Id
                    oRow.Cells(Cols.Nom).Value = oContact.Clx
                    SetDirty()
            End Select

            mDirtyCell = False
        End If
    End Sub


    Private Sub DataGridViewContacts_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewContacts.UserDeletedRow
        SetDirty()
    End Sub

    Private Sub DataGridViewContacts_UserAddedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewContacts.UserAddedRow
        SetDirty()
    End Sub

    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        SetDirty()
    End Sub
End Class
