<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Subscripcio
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
        Me.TabControlNom = New System.Windows.Forms.TabControl()
        Me.TabPageRols = New System.Windows.Forms.TabPage()
        Me.ButtonRemoveRol = New System.Windows.Forms.Button()
        Me.ButtonAddRol = New System.Windows.Forms.Button()
        Me.ListBoxRolAssigned = New System.Windows.Forms.ListBox()
        Me.ListBoxRolSource = New System.Windows.Forms.ListBox()
        Me.TabPageSubscriptors = New System.Windows.Forms.TabPage()
        Me.RadioButtonNone = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAll = New System.Windows.Forms.RadioButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TabPageNom = New System.Windows.Forms.TabPage()
        Me.TabPageDsc = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextNom = New Winforms.Xl_LangsText()
        Me.Xl_LangsTextDsc = New Winforms.Xl_LangsText()
        Me.TabControlNom.SuspendLayout()
        Me.TabPageRols.SuspendLayout()
        Me.TabPageSubscriptors.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabPageNom.SuspendLayout()
        Me.TabPageDsc.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControlNom
        '
        Me.TabControlNom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlNom.Controls.Add(Me.TabPageNom)
        Me.TabControlNom.Controls.Add(Me.TabPageDsc)
        Me.TabControlNom.Controls.Add(Me.TabPageRols)
        Me.TabControlNom.Controls.Add(Me.TabPageSubscriptors)
        Me.TabControlNom.Location = New System.Drawing.Point(1, 38)
        Me.TabControlNom.Name = "TabControlNom"
        Me.TabControlNom.SelectedIndex = 0
        Me.TabControlNom.Size = New System.Drawing.Size(471, 350)
        Me.TabControlNom.TabIndex = 49
        '
        'TabPageRols
        '
        Me.TabPageRols.BackColor = System.Drawing.Color.Transparent
        Me.TabPageRols.Controls.Add(Me.ButtonRemoveRol)
        Me.TabPageRols.Controls.Add(Me.ButtonAddRol)
        Me.TabPageRols.Controls.Add(Me.ListBoxRolAssigned)
        Me.TabPageRols.Controls.Add(Me.ListBoxRolSource)
        Me.TabPageRols.Location = New System.Drawing.Point(4, 22)
        Me.TabPageRols.Name = "TabPageRols"
        Me.TabPageRols.Size = New System.Drawing.Size(463, 324)
        Me.TabPageRols.TabIndex = 3
        Me.TabPageRols.Text = "Rols"
        '
        'ButtonRemoveRol
        '
        Me.ButtonRemoveRol.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemoveRol.Location = New System.Drawing.Point(190, 82)
        Me.ButtonRemoveRol.Name = "ButtonRemoveRol"
        Me.ButtonRemoveRol.Size = New System.Drawing.Size(75, 72)
        Me.ButtonRemoveRol.TabIndex = 3
        Me.ButtonRemoveRol.Text = "<"
        Me.ButtonRemoveRol.UseVisualStyleBackColor = True
        '
        'ButtonAddRol
        '
        Me.ButtonAddRol.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAddRol.Location = New System.Drawing.Point(190, 4)
        Me.ButtonAddRol.Name = "ButtonAddRol"
        Me.ButtonAddRol.Size = New System.Drawing.Size(75, 72)
        Me.ButtonAddRol.TabIndex = 2
        Me.ButtonAddRol.Text = ">"
        Me.ButtonAddRol.UseVisualStyleBackColor = True
        '
        'ListBoxRolAssigned
        '
        Me.ListBoxRolAssigned.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxRolAssigned.FormattingEnabled = True
        Me.ListBoxRolAssigned.Location = New System.Drawing.Point(271, 4)
        Me.ListBoxRolAssigned.Name = "ListBoxRolAssigned"
        Me.ListBoxRolAssigned.Size = New System.Drawing.Size(189, 316)
        Me.ListBoxRolAssigned.TabIndex = 1
        '
        'ListBoxRolSource
        '
        Me.ListBoxRolSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxRolSource.BackColor = System.Drawing.SystemColors.Control
        Me.ListBoxRolSource.FormattingEnabled = True
        Me.ListBoxRolSource.Location = New System.Drawing.Point(4, 4)
        Me.ListBoxRolSource.Name = "ListBoxRolSource"
        Me.ListBoxRolSource.Size = New System.Drawing.Size(180, 316)
        Me.ListBoxRolSource.TabIndex = 0
        '
        'TabPageSubscriptors
        '
        Me.TabPageSubscriptors.Controls.Add(Me.RadioButtonNone)
        Me.TabPageSubscriptors.Controls.Add(Me.RadioButtonAll)
        Me.TabPageSubscriptors.Controls.Add(Me.DataGridView1)
        Me.TabPageSubscriptors.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSubscriptors.Name = "TabPageSubscriptors"
        Me.TabPageSubscriptors.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSubscriptors.Size = New System.Drawing.Size(463, 324)
        Me.TabPageSubscriptors.TabIndex = 4
        Me.TabPageSubscriptors.Text = "Suscriptors"
        Me.TabPageSubscriptors.UseVisualStyleBackColor = True
        '
        'RadioButtonNone
        '
        Me.RadioButtonNone.AutoSize = True
        Me.RadioButtonNone.Location = New System.Drawing.Point(36, 24)
        Me.RadioButtonNone.Name = "RadioButtonNone"
        Me.RadioButtonNone.Size = New System.Drawing.Size(285, 17)
        Me.RadioButtonNone.TabIndex = 2
        Me.RadioButtonNone.Text = "els seguents emails han DENEGAT la seva subscripció"
        Me.RadioButtonNone.UseVisualStyleBackColor = True
        '
        'RadioButtonAll
        '
        Me.RadioButtonAll.AutoSize = True
        Me.RadioButtonAll.Checked = True
        Me.RadioButtonAll.Location = New System.Drawing.Point(36, 7)
        Me.RadioButtonAll.Name = "RadioButtonAll"
        Me.RadioButtonAll.Size = New System.Drawing.Size(239, 17)
        Me.RadioButtonAll.TabIndex = 1
        Me.RadioButtonAll.TabStop = True
        Me.RadioButtonAll.Text = "els seguents emails volen rebre la subscripció"
        Me.RadioButtonAll.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 53)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(457, 268)
        Me.DataGridView1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 390)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(473, 31)
        Me.Panel1.TabIndex = 52
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(365, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TabPageNom
        '
        Me.TabPageNom.Controls.Add(Me.Xl_LangsTextNom)
        Me.TabPageNom.Location = New System.Drawing.Point(4, 22)
        Me.TabPageNom.Name = "TabPageNom"
        Me.TabPageNom.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNom.Size = New System.Drawing.Size(463, 324)
        Me.TabPageNom.TabIndex = 5
        Me.TabPageNom.Text = "Titol"
        Me.TabPageNom.UseVisualStyleBackColor = True
        '
        'TabPageDsc
        '
        Me.TabPageDsc.Controls.Add(Me.Xl_LangsTextDsc)
        Me.TabPageDsc.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDsc.Name = "TabPageDsc"
        Me.TabPageDsc.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDsc.Size = New System.Drawing.Size(463, 324)
        Me.TabPageDsc.TabIndex = 6
        Me.TabPageDsc.Text = "Descripció"
        Me.TabPageDsc.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextNom
        '
        Me.Xl_LangsTextNom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextNom.Location = New System.Drawing.Point(3, 3)
        Me.Xl_LangsTextNom.Name = "Xl_LangsTextNom"
        Me.Xl_LangsTextNom.Size = New System.Drawing.Size(457, 318)
        Me.Xl_LangsTextNom.TabIndex = 0
        '
        'Xl_LangsTextDsc
        '
        Me.Xl_LangsTextDsc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextDsc.Location = New System.Drawing.Point(3, 3)
        Me.Xl_LangsTextDsc.Name = "Xl_LangsTextDsc"
        Me.Xl_LangsTextDsc.Size = New System.Drawing.Size(457, 318)
        Me.Xl_LangsTextDsc.TabIndex = 1
        '
        'Xl_Subscripcio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControlNom)
        Me.Name = "Xl_Subscripcio"
        Me.Size = New System.Drawing.Size(473, 421)
        Me.TabControlNom.ResumeLayout(False)
        Me.TabPageRols.ResumeLayout(False)
        Me.TabPageSubscriptors.ResumeLayout(False)
        Me.TabPageSubscriptors.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TabPageNom.ResumeLayout(False)
        Me.TabPageDsc.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControlNom As System.Windows.Forms.TabControl
    Friend WithEvents TabPageRols As System.Windows.Forms.TabPage
    Friend WithEvents ButtonRemoveRol As System.Windows.Forms.Button
    Friend WithEvents ButtonAddRol As System.Windows.Forms.Button
    Friend WithEvents ListBoxRolAssigned As System.Windows.Forms.ListBox
    Friend WithEvents ListBoxRolSource As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TabPageSubscriptors As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButtonNone As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAll As System.Windows.Forms.RadioButton
    Friend WithEvents TabPageNom As TabPage
    Friend WithEvents Xl_LangsTextNom As Xl_LangsText
    Friend WithEvents TabPageDsc As TabPage
    Friend WithEvents Xl_LangsTextDsc As Xl_LangsText
End Class
