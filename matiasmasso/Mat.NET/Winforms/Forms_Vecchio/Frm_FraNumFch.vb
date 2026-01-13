

Public Class Frm_FraNumFch
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "


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
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextboxNum As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxFch As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNum As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.TextboxNum = New System.Windows.Forms.TextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxFch = New System.Windows.Forms.CheckBox
        Me.CheckBoxNum = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(161, 104)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(88, 24)
        Me.ButtonOk.TabIndex = 2
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(56, 104)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 24)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'TextboxNum
        '
        Me.TextboxNum.Location = New System.Drawing.Point(152, 8)
        Me.TextboxNum.Name = "TextboxNum"
        Me.TextboxNum.Size = New System.Drawing.Size(97, 20)
        Me.TextboxNum.TabIndex = 4
        Me.TextboxNum.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(152, 32)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1.TabIndex = 5
        Me.DateTimePicker1.Visible = False
        '
        'CheckBoxFch
        '
        Me.CheckBoxFch.Location = New System.Drawing.Point(8, 32)
        Me.CheckBoxFch.Name = "CheckBoxFch"
        Me.CheckBoxFch.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxFch.TabIndex = 6
        Me.CheckBoxFch.Text = "fixar data"
        '
        'CheckBoxNum
        '
        Me.CheckBoxNum.Location = New System.Drawing.Point(8, 8)
        Me.CheckBoxNum.Name = "CheckBoxNum"
        Me.CheckBoxNum.Size = New System.Drawing.Size(144, 16)
        Me.CheckBoxNum.TabIndex = 7
        Me.CheckBoxNum.Text = "fixar numero de factura"
        '
        'Frm_FraNumFch
        '
        Me.ClientSize = New System.Drawing.Size(272, 134)
        Me.Controls.Add(Me.CheckBoxNum)
        Me.Controls.Add(Me.CheckBoxFch)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextboxNum)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Name = "Frm_FraNumFch"
        Me.Text = "NUMERO I DATA FACTURA"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mCancel As Boolean

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        'per compatibilitat antiga versio
    End Sub

    Public Sub New(ByVal oInvoice As DTOInvoice)
        MyBase.New()
        Me.InitializeComponent()
        Me.FraNum = oInvoice.Num
        Me.FraFch = oInvoice.Fch
    End Sub


    Public ReadOnly Property Cancel() As Boolean
        Get
            Return mCancel
        End Get
    End Property

    Public Property FraNum() As Integer
        Get
            Dim retval As Integer
            If CheckBoxNum.Checked And Not mCancel Then
                If IsNumeric(TextboxNum.Text) Then retval = CInt(TextboxNum.Text)
            End If
            Return retval
        End Get
        Set(ByVal Value As Integer)
            CheckBoxNum.Checked = True
            TextboxNum.Visible = True
            TextboxNum.Text = Value
        End Set
    End Property

    Public Property FraFch() As Date
        Get
            Dim retval As Date
            If CheckBoxFch.Checked And Not mCancel Then
                retval = DateTimePicker1.Value
            End If
            Return retval
        End Get
        Set(ByVal Value As Date)
            CheckBoxFch.Checked = True
            DateTimePicker1.Visible = True
            DateTimePicker1.Value = Value
        End Set
    End Property

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mCancel = True
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Me.Close()
    End Sub

    Private Sub CheckBoxFch_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFch.CheckedChanged
        DateTimePicker1.Visible = CheckBoxFch.Checked
    End Sub

    Private Sub CheckBoxNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNum.CheckedChanged
        TextboxNum.Visible = CheckBoxNum.Checked
    End Sub
End Class
