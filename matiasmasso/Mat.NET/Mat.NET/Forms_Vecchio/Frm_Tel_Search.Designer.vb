<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Tel_Search
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Tel_Search))
        Me.RadioButtonContacte = New System.Windows.Forms.RadioButton
        Me.RadioButtonNIF = New System.Windows.Forms.RadioButton
        Me.RadioButtonAdr = New System.Windows.Forms.RadioButton
        Me.RadioButtonMail = New System.Windows.Forms.RadioButton
        Me.PictureBoxNotFound = New System.Windows.Forms.PictureBox
        Me.ButtonSearch = New System.Windows.Forms.Button
        Me.TextBoxSearch = New System.Windows.Forms.TextBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.RadioButtonTel = New System.Windows.Forms.RadioButton
        CType(Me.PictureBoxNotFound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButtonContacte
        '
        Me.RadioButtonContacte.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonContacte.AutoSize = True
        Me.RadioButtonContacte.Location = New System.Drawing.Point(568, 144)
        Me.RadioButtonContacte.Name = "RadioButtonContacte"
        Me.RadioButtonContacte.Size = New System.Drawing.Size(67, 17)
        Me.RadioButtonContacte.TabIndex = 6
        Me.RadioButtonContacte.Text = "contacte"
        '
        'RadioButtonNIF
        '
        Me.RadioButtonNIF.AutoSize = True
        Me.RadioButtonNIF.Location = New System.Drawing.Point(568, 121)
        Me.RadioButtonNIF.Name = "RadioButtonNIF"
        Me.RadioButtonNIF.Size = New System.Drawing.Size(42, 17)
        Me.RadioButtonNIF.TabIndex = 5
        Me.RadioButtonNIF.Text = "NIF"
        '
        'RadioButtonAdr
        '
        Me.RadioButtonAdr.AutoSize = True
        Me.RadioButtonAdr.Location = New System.Drawing.Point(568, 98)
        Me.RadioButtonAdr.Name = "RadioButtonAdr"
        Me.RadioButtonAdr.Size = New System.Drawing.Size(58, 17)
        Me.RadioButtonAdr.TabIndex = 4
        Me.RadioButtonAdr.Text = "adreça"
        '
        'RadioButtonMail
        '
        Me.RadioButtonMail.AutoSize = True
        Me.RadioButtonMail.Checked = True
        Me.RadioButtonMail.Location = New System.Drawing.Point(568, 52)
        Me.RadioButtonMail.Name = "RadioButtonMail"
        Me.RadioButtonMail.Size = New System.Drawing.Size(52, 17)
        Me.RadioButtonMail.TabIndex = 2
        Me.RadioButtonMail.Text = "e-mail"
        '
        'PictureBoxNotFound
        '
        Me.PictureBoxNotFound.Image = CType(resources.GetObject("PictureBoxNotFound.Image"), System.Drawing.Image)
        Me.PictureBoxNotFound.Location = New System.Drawing.Point(214, 100)
        Me.PictureBoxNotFound.Name = "PictureBoxNotFound"
        Me.PictureBoxNotFound.Size = New System.Drawing.Size(100, 88)
        Me.PictureBoxNotFound.TabIndex = 11
        Me.PictureBoxNotFound.TabStop = False
        Me.PictureBoxNotFound.Visible = False
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.Location = New System.Drawing.Point(568, 11)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(64, 24)
        Me.ButtonSearch.TabIndex = 1
        Me.ButtonSearch.Text = "BUSCAR"
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.Location = New System.Drawing.Point(6, 11)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(550, 20)
        Me.TextBoxSearch.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(6, 37)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(550, 325)
        Me.DataGridView1.TabIndex = 7
        '
        'RadioButtonTel
        '
        Me.RadioButtonTel.AutoSize = True
        Me.RadioButtonTel.Location = New System.Drawing.Point(568, 75)
        Me.RadioButtonTel.Name = "RadioButtonTel"
        Me.RadioButtonTel.Size = New System.Drawing.Size(55, 17)
        Me.RadioButtonTel.TabIndex = 3
        Me.RadioButtonTel.Text = "tel/fax"
        '
        'Frm_Tel_Search2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 374)
        Me.Controls.Add(Me.RadioButtonTel)
        Me.Controls.Add(Me.PictureBoxNotFound)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.RadioButtonContacte)
        Me.Controls.Add(Me.RadioButtonNIF)
        Me.Controls.Add(Me.RadioButtonAdr)
        Me.Controls.Add(Me.RadioButtonMail)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.TextBoxSearch)
        Me.Name = "Frm_Tel_Search2"
        Me.Text = "BUSCAR TELEFONS"
        CType(Me.PictureBoxNotFound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButtonContacte As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonNIF As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAdr As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonMail As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBoxNotFound As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButtonTel As System.Windows.Forms.RadioButton
End Class
