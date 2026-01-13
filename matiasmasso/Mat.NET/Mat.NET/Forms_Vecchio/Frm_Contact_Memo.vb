

Public Class Frm_Contact_Memo
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
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents PictureBoxLocked As System.Windows.Forms.PictureBox
    Friend WithEvents ComboBoxUsr As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_Contact1 = New Mat.NET.Xl_Contact()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PictureBoxLocked = New System.Windows.Forms.PictureBox()
        Me.ComboBoxUsr = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(72, 8)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(368, 20)
        Me.Xl_Contact1.TabIndex = 0
        Me.Xl_Contact1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "nom:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "data:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(72, 32)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 3
        Me.DateTimePicker1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(72, 151)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(368, 168)
        Me.TextBox1.TabIndex = 4
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(344, 351)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(96, 24)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 351)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 24)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonDel
        '
        Me.ButtonDel.Location = New System.Drawing.Point(8, 351)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(96, 24)
        Me.ButtonDel.TabIndex = 8
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'PictureBoxLocked
        '
        Me.PictureBoxLocked.Image = Global.Mat.NET.My.Resources.Resources.candado
        Me.PictureBoxLocked.Location = New System.Drawing.Point(424, 36)
        Me.PictureBoxLocked.Name = "PictureBoxLocked"
        Me.PictureBoxLocked.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxLocked.TabIndex = 9
        Me.PictureBoxLocked.TabStop = False
        Me.PictureBoxLocked.Visible = False
        '
        'ComboBoxUsr
        '
        Me.ComboBoxUsr.FormattingEnabled = True
        Me.ComboBoxUsr.Location = New System.Drawing.Point(172, 34)
        Me.ComboBoxUsr.Name = "ComboBoxUsr"
        Me.ComboBoxUsr.Size = New System.Drawing.Size(172, 21)
        Me.ComboBoxUsr.TabIndex = 10
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Despatx", "Comercial", "Impagos"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(172, 61)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(172, 21)
        Me.ComboBoxCod.TabIndex = 11
        '
        'Frm_Contact_Memo
        '
        Me.ClientSize = New System.Drawing.Size(448, 379)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.ComboBoxUsr)
        Me.Controls.Add(Me.PictureBoxLocked)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Name = "Frm_Contact_Memo"
        Me.Text = "MEMO"
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mMem As Mem
    Private mAllowEvents As Boolean
    Private mLocked As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMem As Mem)
        MyBase.New()
        Me.InitializeComponent()
        mMem = oMem
        ButtonOk.Enabled = False
        LoadUsrs()
        With mMem
            DateTimePicker1.Value = .Fch
            Xl_Contact1.Contact = .Contact
            TextBox1.Text = .Text
        End With

        If mMem.Id = 0 Then
            Me.Text = "NOU MEMO"
            ButtonDel.Enabled = False
            ComboBoxUsr.SelectedValue = root.Usuari.Id
            Dim oUsr As Contact = MaxiSrvr.Contact.FromNum(mMem.Emp, ComboBoxUsr.SelectedValue)
            Select Case oUsr.Rol.Id
                Case Rol.Ids.Comercial, Rol.Ids.Rep
                    If mMem.Cod = DTOMem.Cods.Despaitx Then
                        mMem.Cod = DTOMem.Cods.Rep
                    End If
                Case Else
                    'mMem.Cod = DTOMem.Cods.Despaitx
            End Select
        Else
            Me.Text = "MEMO #" & mMem.Id
            PictureBoxLocked.Visible = mLocked
            ButtonDel.Enabled = Not mLocked
            ComboBoxUsr.SelectedValue = mMem.Usr.Id
            ButtonCancel.Focus()
        End If

        ComboBoxCod.SelectedIndex = mMem.Cod

        Select Case BLL.BLLSession.Current.User.Rol.id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
            Case Else
                ComboBoxUsr.Enabled = False
        End Select
        mAllowEvents = True
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Not Xl_Contact1.Contact.Exists Then
            MsgBox("contacte incorrecte", MsgBoxStyle.Exclamation, "MAT.NET")
            Exit Sub
        End If
        Dim oUsr As Contact = MaxiSrvr.Contact.FromNum(mMem.Emp, ComboBoxUsr.SelectedValue)
        With mMem
            .Fch = DateTimePicker1.Value
            .Contact = Xl_Contact1.Contact
            .Text = TextBox1.Text
            .Cod = ComboBoxCod.SelectedIndex
            .Update(oUsr)
            Me.Close()
            RaiseEvent AfterUpdate(mMem, New System.EventArgs)
        End With
    End Sub

    Private Sub SetButtons()
        If ((Not mLocked) Or mMem.Id = 0) Then
            If TextBox1.Text > "" Then
                ButtonOk.Enabled = True
            End If
        End If
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate, _
     TextBox1.TextChanged, _
      ComboBoxCod.SelectedIndexChanged, _
       ComboBoxUsr.SelectedIndexChanged, _
        DateTimePicker1.ValueChanged

        If mAllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest memo?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            mMem.delete()
            Me.Close()
            RaiseEvent AfterUpdate(mMem, New System.EventArgs)
        End If
    End Sub

    Private Sub LoadUsrs()
        Dim SQL As String = "SELECT EmpUsr.cli, Usr.login " _
        & "FROM  EmpUsr INNER JOIN " _
        & "Usr ON EmpUsr.UsrGuid = Usr.Guid " _
        & "WHERE EmpUsr.emp =" & mMem.Emp.Id & " " _
        & "ORDER BY Usr.login"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxUsr
            .DataSource = oDs.Tables(0)
            .DisplayMember = "login"
            .ValueMember = "cli"
        End With
    End Sub
End Class
