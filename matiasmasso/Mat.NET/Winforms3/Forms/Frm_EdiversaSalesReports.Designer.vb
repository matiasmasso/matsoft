<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EdiversaSalesReports
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_EdiversaSalesReportsHeaders1 = New Mat.Net.Xl_EdiversaSalesReportsHeaders()
        Me.Xl_EdiversaSalesReportItems1 = New Mat.Net.Xl_EdiversaSalesReportItems()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.ComboBoxCustomer = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_EdiversaSalesReportsHeaders1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_EdiversaSalesReportItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_EdiversaSalesReportsHeaders1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_EdiversaSalesReportItems1)
        Me.SplitContainer1.Size = New System.Drawing.Size(604, 240)
        Me.SplitContainer1.SplitterDistance = 143
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_EdiversaSalesReportsHeaders1
        '
        Me.Xl_EdiversaSalesReportsHeaders1.AllowUserToAddRows = False
        Me.Xl_EdiversaSalesReportsHeaders1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaSalesReportsHeaders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaSalesReportsHeaders1.DisplayObsolets = False
        Me.Xl_EdiversaSalesReportsHeaders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaSalesReportsHeaders1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaSalesReportsHeaders1.MouseIsDown = False
        Me.Xl_EdiversaSalesReportsHeaders1.Name = "Xl_EdiversaSalesReportsHeaders1"
        Me.Xl_EdiversaSalesReportsHeaders1.ReadOnly = True
        Me.Xl_EdiversaSalesReportsHeaders1.Size = New System.Drawing.Size(143, 240)
        Me.Xl_EdiversaSalesReportsHeaders1.TabIndex = 0
        '
        'Xl_EdiversaSalesReportItems1
        '
        Me.Xl_EdiversaSalesReportItems1.AllowUserToAddRows = False
        Me.Xl_EdiversaSalesReportItems1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaSalesReportItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaSalesReportItems1.DisplayObsolets = False
        Me.Xl_EdiversaSalesReportItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaSalesReportItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EdiversaSalesReportItems1.MouseIsDown = False
        Me.Xl_EdiversaSalesReportItems1.Name = "Xl_EdiversaSalesReportItems1"
        Me.Xl_EdiversaSalesReportItems1.ReadOnly = True
        Me.Xl_EdiversaSalesReportItems1.Size = New System.Drawing.Size(457, 240)
        Me.Xl_EdiversaSalesReportItems1.TabIndex = 0
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(441, 10)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 1
        Me.Xl_Years1.Value = 0
        '
        'ComboBoxCustomer
        '
        Me.ComboBoxCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCustomer.FormattingEnabled = True
        Me.ComboBoxCustomer.Location = New System.Drawing.Point(0, 10)
        Me.ComboBoxCustomer.Name = "ComboBoxCustomer"
        Me.ComboBoxCustomer.Size = New System.Drawing.Size(435, 21)
        Me.ComboBoxCustomer.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(604, 263)
        Me.Panel1.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 240)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(604, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_EdiversaSalesReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 302)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ComboBoxCustomer)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Name = "Frm_EdiversaSalesReports"
        Me.Text = "Edi Sales Reports"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_EdiversaSalesReportsHeaders1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_EdiversaSalesReportItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents ComboBoxCustomer As ComboBox
    Friend WithEvents Xl_EdiversaSalesReportsHeaders1 As Xl_EdiversaSalesReportsHeaders
    Friend WithEvents Xl_EdiversaSalesReportItems1 As Xl_EdiversaSalesReportItems
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
