<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CliCtas
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Contact_Logo1 = New Mat.Net.Xl_Contact_Logo()
        Me.Xl_Exercicis1 = New Mat.Net.Xl_Exercicis()
        Me.Xl_Extractes1 = New Mat.Net.Xl_Extractes()
        Me.Xl_Extracte1 = New Mat.Net.Xl_Extracte()
        Me.Xl_Contact_Pnds1 = New Mat.Net.Xl_Contact_Pnds_Old()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(842, 259)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(834, 233)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Comptes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Contact_Pnds1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(834, 233)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Partides pendents"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Exercicis1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(828, 227)
        Me.SplitContainer1.SplitterDistance = 80
        Me.SplitContainer1.TabIndex = 2
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Xl_Extractes1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Extracte1)
        Me.SplitContainer2.Size = New System.Drawing.Size(744, 227)
        Me.SplitContainer2.SplitterDistance = 173
        Me.SplitContainer2.TabIndex = 1
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(688, 5)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 3
        Me.Xl_Contact_Logo1.Visible = False
        '
        'Xl_Exercicis1
        '
        Me.Xl_Exercicis1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Exercicis1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Exercicis1.Name = "Xl_Exercicis1"
        Me.Xl_Exercicis1.Size = New System.Drawing.Size(80, 227)
        Me.Xl_Exercicis1.TabIndex = 0
        '
        'Xl_Extractes1
        '
        Me.Xl_Extractes1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extractes1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extractes1.Name = "Xl_Extractes1"
        Me.Xl_Extractes1.Size = New System.Drawing.Size(173, 227)
        Me.Xl_Extractes1.TabIndex = 0
        '
        'Xl_Extracte1
        '
        Me.Xl_Extracte1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Extracte1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Extracte1.Name = "Xl_Extracte1"
        Me.Xl_Extracte1.Size = New System.Drawing.Size(567, 227)
        Me.Xl_Extracte1.TabIndex = 0
        '
        'Xl_Contact_Pnds1
        '
        Me.Xl_Contact_Pnds1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Pnds1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Contact_Pnds1.Name = "Xl_Contact_Pnds1"
        Me.Xl_Contact_Pnds1.Size = New System.Drawing.Size(828, 227)
        Me.Xl_Contact_Pnds1.TabIndex = 0
        '
        'Frm_CliCtas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 297)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_CliCtas"
        Me.Text = "Comptes"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Extractes1 As Xl_Extractes
    Friend WithEvents Xl_Extracte1 As Xl_Extracte
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Exercicis1 As Mat.Net.Xl_Exercicis
    Friend WithEvents Xl_Contact_Logo1 As Mat.Net.Xl_Contact_Logo
    Friend WithEvents Xl_Contact_Pnds1 As Mat.Net.Xl_Contact_Pnds_Old
End Class
