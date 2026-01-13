<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AlbTrps
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
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxZona = New System.Windows.Forms.TextBox()
        Me.TextBoxKg = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxEur = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxAlb = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_DeliveryTrps1 = New Winforms.Xl_DeliveryTrps()
        CType(Me.Xl_DeliveryTrps1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(76, 66)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.ReadOnly = True
        Me.TextBoxM3.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxM3.TabIndex = 9
        Me.TextBoxM3.TabStop = False
        Me.TextBoxM3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Volumen m3:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Zona:"
        '
        'TextBoxZona
        '
        Me.TextBoxZona.Location = New System.Drawing.Point(76, 40)
        Me.TextBoxZona.Name = "TextBoxZona"
        Me.TextBoxZona.ReadOnly = True
        Me.TextBoxZona.Size = New System.Drawing.Size(362, 20)
        Me.TextBoxZona.TabIndex = 11
        Me.TextBoxZona.TabStop = False
        '
        'TextBoxKg
        '
        Me.TextBoxKg.Location = New System.Drawing.Point(195, 67)
        Me.TextBoxKg.Name = "TextBoxKg"
        Me.TextBoxKg.ReadOnly = True
        Me.TextBoxKg.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxKg.TabIndex = 13
        Me.TextBoxKg.TabStop = False
        Me.TextBoxKg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(147, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Pes Kg:"
        '
        'TextBoxEur
        '
        Me.TextBoxEur.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEur.Location = New System.Drawing.Point(322, 66)
        Me.TextBoxEur.Name = "TextBoxEur"
        Me.TextBoxEur.ReadOnly = True
        Me.TextBoxEur.Size = New System.Drawing.Size(116, 20)
        Me.TextBoxEur.TabIndex = 15
        Me.TextBoxEur.TabStop = False
        Me.TextBoxEur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(274, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Import:"
        '
        'TextBoxAlb
        '
        Me.TextBoxAlb.Location = New System.Drawing.Point(76, 14)
        Me.TextBoxAlb.Name = "TextBoxAlb"
        Me.TextBoxAlb.ReadOnly = True
        Me.TextBoxAlb.Size = New System.Drawing.Size(362, 20)
        Me.TextBoxAlb.TabIndex = 18
        Me.TextBoxAlb.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(4, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 16)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Albarà:"
        '
        'Xl_DeliveryTrps1
        '
        Me.Xl_DeliveryTrps1.AllowUserToAddRows = False
        Me.Xl_DeliveryTrps1.AllowUserToDeleteRows = False
        Me.Xl_DeliveryTrps1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DeliveryTrps1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DeliveryTrps1.DisplayObsolets = False
        Me.Xl_DeliveryTrps1.Location = New System.Drawing.Point(2, 106)
        Me.Xl_DeliveryTrps1.Name = "Xl_DeliveryTrps1"
        Me.Xl_DeliveryTrps1.ReadOnly = True
        Me.Xl_DeliveryTrps1.Size = New System.Drawing.Size(436, 272)
        Me.Xl_DeliveryTrps1.TabIndex = 16
        '
        'Frm_AlbTrps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 378)
        Me.Controls.Add(Me.TextBoxAlb)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_DeliveryTrps1)
        Me.Controls.Add(Me.TextBoxEur)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxKg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxZona)
        Me.Controls.Add(Me.TextBoxM3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_AlbTrps"
        Me.Text = "Comparativa transportistes"
        CType(Me.Xl_DeliveryTrps1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxZona As TextBox
    Friend WithEvents TextBoxKg As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxEur As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_DeliveryTrps1 As Xl_DeliveryTrps
    Friend WithEvents TextBoxAlb As TextBox
    Friend WithEvents Label5 As Label
End Class
