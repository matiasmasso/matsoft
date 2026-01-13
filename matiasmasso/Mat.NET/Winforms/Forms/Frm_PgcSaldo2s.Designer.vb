<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PgcSaldo2s
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Xl_PgcSaldo2s1 = New Winforms.Xl_PgcSaldo2s()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker1a = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1b = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker2b = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2a = New System.Windows.Forms.DateTimePicker()
        CType(Me.Xl_PgcSaldo2s1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_PgcSaldo2s1
        '
        Me.Xl_PgcSaldo2s1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PgcSaldo2s1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PgcSaldo2s1.DisplayObsolets = False
        Me.Xl_PgcSaldo2s1.Location = New System.Drawing.Point(1, 123)
        Me.Xl_PgcSaldo2s1.Name = "Xl_PgcSaldo2s1"
        Me.Xl_PgcSaldo2s1.Size = New System.Drawing.Size(549, 220)
        Me.Xl_PgcSaldo2s1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1b)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1a)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(239, 106)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Periode 1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker2b)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker2a)
        Me.GroupBox2.Controls.Add(Me.ComboBox2)
        Me.GroupBox2.Location = New System.Drawing.Point(252, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(239, 105)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Periode 2"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"mes anterior", "mateix mes any anterior", "trimestre anterior", "mateix trimestre any anterior", "personalitzat"})
        Me.ComboBox1.Location = New System.Drawing.Point(7, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(226, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Mes actual", "Ultim mes", "Trimestre actual", "Ultim trimestre", "Personalitzat"})
        Me.ComboBox2.Location = New System.Drawing.Point(6, 18)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(226, 21)
        Me.ComboBox2.TabIndex = 1
        '
        'DateTimePicker1a
        '
        Me.DateTimePicker1a.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1a.Location = New System.Drawing.Point(136, 46)
        Me.DateTimePicker1a.Name = "DateTimePicker1a"
        Me.DateTimePicker1a.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1a.TabIndex = 1
        '
        'DateTimePicker1b
        '
        Me.DateTimePicker1b.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1b.Location = New System.Drawing.Point(136, 71)
        Me.DateTimePicker1b.Name = "DateTimePicker1b"
        Me.DateTimePicker1b.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1b.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(94, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "des de"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(94, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "fins"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(93, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "fins"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(93, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "des de"
        '
        'DateTimePicker2b
        '
        Me.DateTimePicker2b.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2b.Location = New System.Drawing.Point(135, 70)
        Me.DateTimePicker2b.Name = "DateTimePicker2b"
        Me.DateTimePicker2b.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker2b.TabIndex = 6
        '
        'DateTimePicker2a
        '
        Me.DateTimePicker2a.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2a.Location = New System.Drawing.Point(135, 45)
        Me.DateTimePicker2a.Name = "DateTimePicker2a"
        Me.DateTimePicker2a.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker2a.TabIndex = 5
        '
        'Frm_PgcSaldo2s
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 346)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_PgcSaldo2s1)
        Me.Name = "Frm_PgcSaldo2s"
        Me.Text = "Comparativa de saldos"
        CType(Me.Xl_PgcSaldo2s1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PgcSaldo2s1 As Xl_PgcSaldo2s
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePicker1b As DateTimePicker
    Friend WithEvents DateTimePicker1a As DateTimePicker
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePicker2b As DateTimePicker
    Friend WithEvents DateTimePicker2a As DateTimePicker
    Friend WithEvents ComboBox2 As ComboBox
End Class
