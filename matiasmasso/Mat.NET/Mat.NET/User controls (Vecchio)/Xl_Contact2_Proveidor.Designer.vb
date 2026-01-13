<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact2_Proveidor
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
        Me.CheckBoxImportPrv = New System.Windows.Forms.CheckBox
        Me.GroupBoxImport = New System.Windows.Forms.GroupBox
        Me.Xl_CodiMercancia1 = New Xl_CodiMercancia
        Me.Label1CodiMercancia = New System.Windows.Forms.Label
        Me.ComboBoxIncoterms = New System.Windows.Forms.ComboBox
        Me.Label1Incoterms = New System.Windows.Forms.Label
        Me.Xl_FormaDePago1 = New Xl_FormaDePago
        Me.Xl_Cur1 = New Xl_Cur
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_Cta1 = New Xl_Cta
        Me.GroupBoxImport.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxImportPrv
        '
        Me.CheckBoxImportPrv.AutoSize = True
        Me.CheckBoxImportPrv.Location = New System.Drawing.Point(56, 126)
        Me.CheckBoxImportPrv.Name = "CheckBoxImportPrv"
        Me.CheckBoxImportPrv.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxImportPrv.TabIndex = 74
        Me.CheckBoxImportPrv.Text = "Mercancía importació"
        Me.CheckBoxImportPrv.UseVisualStyleBackColor = True
        '
        'GroupBoxImport
        '
        Me.GroupBoxImport.Controls.Add(Me.Xl_CodiMercancia1)
        Me.GroupBoxImport.Controls.Add(Me.Label1CodiMercancia)
        Me.GroupBoxImport.Controls.Add(Me.ComboBoxIncoterms)
        Me.GroupBoxImport.Controls.Add(Me.Label1Incoterms)
        Me.GroupBoxImport.Location = New System.Drawing.Point(56, 138)
        Me.GroupBoxImport.Name = "GroupBoxImport"
        Me.GroupBoxImport.Size = New System.Drawing.Size(365, 79)
        Me.GroupBoxImport.TabIndex = 73
        Me.GroupBoxImport.TabStop = False
        '
        'Xl_CodiMercancia1
        '
        Me.Xl_CodiMercancia1.Location = New System.Drawing.Point(70, 45)
        Me.Xl_CodiMercancia1.Name = "Xl_CodiMercancia1"
        Me.Xl_CodiMercancia1.Size = New System.Drawing.Size(289, 20)
        Me.Xl_CodiMercancia1.TabIndex = 65
        '
        'Label1CodiMercancia
        '
        Me.Label1CodiMercancia.AutoSize = True
        Me.Label1CodiMercancia.Location = New System.Drawing.Point(6, 48)
        Me.Label1CodiMercancia.Name = "Label1CodiMercancia"
        Me.Label1CodiMercancia.Size = New System.Drawing.Size(56, 13)
        Me.Label1CodiMercancia.TabIndex = 60
        Me.Label1CodiMercancia.Text = "mercancia"
        '
        'ComboBoxIncoterms
        '
        Me.ComboBoxIncoterms.FormattingEnabled = True
        Me.ComboBoxIncoterms.Location = New System.Drawing.Point(70, 18)
        Me.ComboBoxIncoterms.Name = "ComboBoxIncoterms"
        Me.ComboBoxIncoterms.Size = New System.Drawing.Size(60, 21)
        Me.ComboBoxIncoterms.TabIndex = 64
        '
        'Label1Incoterms
        '
        Me.Label1Incoterms.AutoSize = True
        Me.Label1Incoterms.Location = New System.Drawing.Point(6, 21)
        Me.Label1Incoterms.Name = "Label1Incoterms"
        Me.Label1Incoterms.Size = New System.Drawing.Size(52, 13)
        Me.Label1Incoterms.TabIndex = 63
        Me.Label1Incoterms.Text = "incoterms"
        '
        'Xl_FormaDePago1
        '
        Me.Xl_FormaDePago1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Xl_FormaDePago1.Location = New System.Drawing.Point(56, 243)
        Me.Xl_FormaDePago1.Name = "Xl_FormaDePago1"
        Me.Xl_FormaDePago1.Size = New System.Drawing.Size(284, 130)
        Me.Xl_FormaDePago1.TabIndex = 72
        '
        'Xl_Cur1
        '
        Me.Xl_Cur1.Cur = Nothing
        Me.Xl_Cur1.Location = New System.Drawing.Point(117, 87)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.Size = New System.Drawing.Size(30, 20)
        Me.Xl_Cur1.TabIndex = 71
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(53, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 70
        Me.Label2.Text = "Divisa:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(53, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Compte:"
        '
        'Xl_Cta1
        '
        Me.Xl_Cta1.Cta = Nothing
        Me.Xl_Cta1.Location = New System.Drawing.Point(117, 63)
        Me.Xl_Cta1.Name = "Xl_Cta1"
        Me.Xl_Cta1.Size = New System.Drawing.Size(136, 20)
        Me.Xl_Cta1.TabIndex = 68
        '
        'Xl_Contact2_Proveidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxImportPrv)
        Me.Controls.Add(Me.GroupBoxImport)
        Me.Controls.Add(Me.Xl_FormaDePago1)
        Me.Controls.Add(Me.Xl_Cur1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Cta1)
        Me.Name = "Xl_Contact2_Proveidor"
        Me.Size = New System.Drawing.Size(474, 437)
        Me.GroupBoxImport.ResumeLayout(False)
        Me.GroupBoxImport.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxImportPrv As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxImport As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_CodiMercancia1 As Xl_CodiMercancia
    Friend WithEvents Label1CodiMercancia As System.Windows.Forms.Label
    Friend WithEvents ComboBoxIncoterms As System.Windows.Forms.ComboBox
    Friend WithEvents Label1Incoterms As System.Windows.Forms.Label
    Friend WithEvents Xl_FormaDePago1 As Xl_FormaDePago
    Friend WithEvents Xl_Cur1 As Xl_Cur
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cta1 As Xl_Cta

End Class
