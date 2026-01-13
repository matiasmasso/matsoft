Public Class Wz_NewCli
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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents TabPageGral As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Gral1 As Xl_Contact_Gral
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_Contact_Cli1 As Xl_Contact_Cli
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.ButtonPrevious = New System.Windows.Forms.Button
        Me.ButtonEnd = New System.Windows.Forms.Button
        Me.ButtonNext = New System.Windows.Forms.Button
        Me.TabPageGral = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Gral1 = New Xl_Contact_Gral
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Xl_Contact_Cli1 = New Xl_Contact_Cli
        Me.TabControl1.SuspendLayout()
        Me.TabPageGral.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGral)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(496, 404)
        Me.TabControl1.TabIndex = 0
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(0, 420)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(96, 24)
        Me.ButtonPrevious.TabIndex = 7
        Me.ButtonPrevious.Text = "< ENRERA"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Enabled = False
        Me.ButtonEnd.Location = New System.Drawing.Point(408, 420)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(96, 24)
        Me.ButtonEnd.TabIndex = 9
        Me.ButtonEnd.Text = "FI >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Enabled = False
        Me.ButtonNext.Location = New System.Drawing.Point(304, 420)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(96, 24)
        Me.ButtonNext.TabIndex = 8
        Me.ButtonNext.Text = "SEGÜENT >"
        '
        'TabPageGral
        '
        Me.TabPageGral.Controls.Add(Me.Xl_Contact_Gral1)
        Me.TabPageGral.Location = New System.Drawing.Point(4, 22)
        Me.TabPageGral.Name = "TabPageGral"
        Me.TabPageGral.Size = New System.Drawing.Size(456, 354)
        Me.TabPageGral.TabIndex = 0
        Me.TabPageGral.Text = "GENERAL"
        '
        'Xl_Contact_Gral1
        '
        Me.Xl_Contact_Gral1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Gral1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Gral1.Name = "Xl_Contact_Gral1"
        Me.Xl_Contact_Gral1.Size = New System.Drawing.Size(456, 354)
        Me.Xl_Contact_Gral1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Contact_Cli1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(488, 378)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "CLIENT"
        '
        'Xl_Contact_Cli1
        '
        Me.Xl_Contact_Cli1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Contact_Cli1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Cli1.Name = "Xl_Contact_Cli1"
        Me.Xl_Contact_Cli1.Size = New System.Drawing.Size(488, 378)
        Me.Xl_Contact_Cli1.TabIndex = 0
        '
        'Wz_NewCli
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 445)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Wz_NewCli"
        Me.Text = "CLIENT NOU"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGral.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
