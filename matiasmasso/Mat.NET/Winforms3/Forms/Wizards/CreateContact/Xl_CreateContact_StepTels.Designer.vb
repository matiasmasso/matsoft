<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_CreateContact_StepTels
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Xl_Tels1 = New Xl_Tels()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonEmail = New System.Windows.Forms.Button()
        Me.ButtonFax = New System.Windows.Forms.Button()
        Me.ButtonMobile = New System.Windows.Forms.Button()
        Me.ButtonTel = New System.Windows.Forms.Button()
        CType(Me.Xl_Tels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Tels1
        '
        Me.Xl_Tels1.AllowUserToAddRows = False
        Me.Xl_Tels1.AllowUserToDeleteRows = False
        Me.Xl_Tels1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Tels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Tels1.DisplayObsolets = False
        Me.Xl_Tels1.Location = New System.Drawing.Point(18, 90)
        Me.Xl_Tels1.MouseIsDown = False
        Me.Xl_Tels1.Name = "Xl_Tels1"
        Me.Xl_Tels1.ReadOnly = True
        Me.Xl_Tels1.Size = New System.Drawing.Size(309, 253)
        Me.Xl_Tels1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "afegir telefons i adreçes email:"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonEmail, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonFax, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonMobile, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonTel, 0, 0)
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(18, 36)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(309, 48)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'ButtonEmail
        '
        Me.ButtonEmail.AllowDrop = True
        Me.ButtonEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonEmail.Image = Global.Mat.Net.My.Resources.Resources.Message
        Me.ButtonEmail.Location = New System.Drawing.Point(234, 3)
        Me.ButtonEmail.Name = "ButtonEmail"
        Me.ButtonEmail.Size = New System.Drawing.Size(72, 42)
        Me.ButtonEmail.TabIndex = 3
        Me.ButtonEmail.Text = "Email"
        Me.ButtonEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonEmail.UseVisualStyleBackColor = True
        '
        'ButtonFax
        '
        Me.ButtonFax.AllowDrop = True
        Me.ButtonFax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonFax.Image = Global.Mat.Net.My.Resources.Resources.Fax2
        Me.ButtonFax.Location = New System.Drawing.Point(157, 3)
        Me.ButtonFax.Name = "ButtonFax"
        Me.ButtonFax.Size = New System.Drawing.Size(71, 42)
        Me.ButtonFax.TabIndex = 2
        Me.ButtonFax.Text = "Fax"
        Me.ButtonFax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonFax.UseVisualStyleBackColor = True
        '
        'ButtonMobile
        '
        Me.ButtonMobile.AllowDrop = True
        Me.ButtonMobile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonMobile.Image = Global.Mat.Net.My.Resources.Resources.CellPhone
        Me.ButtonMobile.Location = New System.Drawing.Point(80, 3)
        Me.ButtonMobile.Name = "ButtonMobile"
        Me.ButtonMobile.Size = New System.Drawing.Size(71, 42)
        Me.ButtonMobile.TabIndex = 1
        Me.ButtonMobile.Text = "Mobil"
        Me.ButtonMobile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonMobile.UseVisualStyleBackColor = True
        '
        'ButtonTel
        '
        Me.ButtonTel.AllowDrop = True
        Me.ButtonTel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonTel.Image = Global.Mat.Net.My.Resources.Resources.Phone
        Me.ButtonTel.Location = New System.Drawing.Point(3, 3)
        Me.ButtonTel.Name = "ButtonTel"
        Me.ButtonTel.Size = New System.Drawing.Size(71, 42)
        Me.ButtonTel.TabIndex = 0
        Me.ButtonTel.Text = "Telèfon"
        Me.ButtonTel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ButtonTel.UseVisualStyleBackColor = True
        '
        'Xl_CreateContact_StepTels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Tels1)
        Me.Name = "Xl_CreateContact_StepTels"
        Me.Size = New System.Drawing.Size(347, 357)
        CType(Me.Xl_Tels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Tels1 As Xl_Tels
    Friend WithEvents Label1 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ButtonEmail As Button
    Friend WithEvents ButtonFax As Button
    Friend WithEvents ButtonMobile As Button
    Friend WithEvents ButtonTel As Button
End Class
