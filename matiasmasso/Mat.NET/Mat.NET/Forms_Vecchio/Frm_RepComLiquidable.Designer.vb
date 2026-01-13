<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepComLiquidable
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxRep = New System.Windows.Forms.TextBox()
        Me.TextBoxFra = New System.Windows.Forms.TextBox()
        Me.LabelFra = New System.Windows.Forms.Label()
        Me.TextBoxRepLiq = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonZoomRep = New System.Windows.Forms.Button()
        Me.ButtonZoomFra = New System.Windows.Forms.Button()
        Me.ButtonZoomRepLiq = New System.Windows.Forms.Button()
        Me.Xl_RepComLiquidableArcs1 = New Mat.NET.Xl_RepComLiquidableArcs()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.CheckBoxLiquidable = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 458)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(692, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(473, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(584, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Comisionista"
        '
        'TextBoxRep
        '
        Me.TextBoxRep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRep.Location = New System.Drawing.Point(84, 20)
        Me.TextBoxRep.Name = "TextBoxRep"
        Me.TextBoxRep.ReadOnly = True
        Me.TextBoxRep.Size = New System.Drawing.Size(560, 20)
        Me.TextBoxRep.TabIndex = 43
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFra.Location = New System.Drawing.Point(84, 46)
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.ReadOnly = True
        Me.TextBoxFra.Size = New System.Drawing.Size(560, 20)
        Me.TextBoxFra.TabIndex = 45
        '
        'LabelFra
        '
        Me.LabelFra.AutoSize = True
        Me.LabelFra.Location = New System.Drawing.Point(13, 49)
        Me.LabelFra.Name = "LabelFra"
        Me.LabelFra.Size = New System.Drawing.Size(43, 13)
        Me.LabelFra.TabIndex = 44
        Me.LabelFra.Text = "Factura"
        '
        'TextBoxRepLiq
        '
        Me.TextBoxRepLiq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRepLiq.Location = New System.Drawing.Point(84, 72)
        Me.TextBoxRepLiq.Name = "TextBoxRepLiq"
        Me.TextBoxRepLiq.ReadOnly = True
        Me.TextBoxRepLiq.Size = New System.Drawing.Size(560, 20)
        Me.TextBoxRepLiq.TabIndex = 47
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Liquidació"
        '
        'ButtonZoomRep
        '
        Me.ButtonZoomRep.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonZoomRep.Location = New System.Drawing.Point(651, 21)
        Me.ButtonZoomRep.Name = "ButtonZoomRep"
        Me.ButtonZoomRep.Size = New System.Drawing.Size(35, 20)
        Me.ButtonZoomRep.TabIndex = 49
        Me.ButtonZoomRep.Text = "..."
        Me.ButtonZoomRep.UseVisualStyleBackColor = True
        '
        'ButtonZoomFra
        '
        Me.ButtonZoomFra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonZoomFra.Location = New System.Drawing.Point(650, 47)
        Me.ButtonZoomFra.Name = "ButtonZoomFra"
        Me.ButtonZoomFra.Size = New System.Drawing.Size(35, 20)
        Me.ButtonZoomFra.TabIndex = 50
        Me.ButtonZoomFra.Text = "..."
        Me.ButtonZoomFra.UseVisualStyleBackColor = True
        '
        'ButtonZoomRepLiq
        '
        Me.ButtonZoomRepLiq.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonZoomRepLiq.Location = New System.Drawing.Point(650, 73)
        Me.ButtonZoomRepLiq.Name = "ButtonZoomRepLiq"
        Me.ButtonZoomRepLiq.Size = New System.Drawing.Size(35, 20)
        Me.ButtonZoomRepLiq.TabIndex = 51
        Me.ButtonZoomRepLiq.Text = "..."
        Me.ButtonZoomRepLiq.UseVisualStyleBackColor = True
        '
        'Xl_RepComLiquidableArcs1
        '
        Me.Xl_RepComLiquidableArcs1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_RepComLiquidableArcs1.Location = New System.Drawing.Point(0, 147)
        Me.Xl_RepComLiquidableArcs1.Name = "Xl_RepComLiquidableArcs1"
        Me.Xl_RepComLiquidableArcs1.Size = New System.Drawing.Size(692, 309)
        Me.Xl_RepComLiquidableArcs1.TabIndex = 52
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Observacions"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(84, 98)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(601, 20)
        Me.TextBoxObs.TabIndex = 56
        '
        'CheckBoxLiquidable
        '
        Me.CheckBoxLiquidable.AutoSize = True
        Me.CheckBoxLiquidable.Location = New System.Drawing.Point(84, 124)
        Me.CheckBoxLiquidable.Name = "CheckBoxLiquidable"
        Me.CheckBoxLiquidable.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxLiquidable.TabIndex = 57
        Me.CheckBoxLiquidable.Text = "Liquidable"
        Me.CheckBoxLiquidable.UseVisualStyleBackColor = True
        '
        'Frm_RepComLiquidable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 489)
        Me.Controls.Add(Me.CheckBoxLiquidable)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_RepComLiquidableArcs1)
        Me.Controls.Add(Me.ButtonZoomRepLiq)
        Me.Controls.Add(Me.ButtonZoomFra)
        Me.Controls.Add(Me.ButtonZoomRep)
        Me.Controls.Add(Me.TextBoxRepLiq)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxFra)
        Me.Controls.Add(Me.LabelFra)
        Me.Controls.Add(Me.TextBoxRep)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_RepComLiquidable"
        Me.Text = "Comisió liquidable"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRep As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents LabelFra As System.Windows.Forms.Label
    Friend WithEvents TextBoxRepLiq As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonZoomRep As System.Windows.Forms.Button
    Friend WithEvents ButtonZoomFra As System.Windows.Forms.Button
    Friend WithEvents ButtonZoomRepLiq As System.Windows.Forms.Button
    Friend WithEvents Xl_RepComLiquidableArcs1 As Mat.NET.Xl_RepComLiquidableArcs
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxLiquidable As System.Windows.Forms.CheckBox
End Class
