<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ProductCategoryStats
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TrackBarFrom = New System.Windows.Forms.TrackBar()
        Me.TrackBarTo = New System.Windows.Forms.TrackBar()
        Me.LabelFchFrom = New System.Windows.Forms.Label()
        Me.LabelFchTo = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_ProductStats1 = New Xl_ProductStats()
        CType(Me.TrackBarFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ProductStats1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrackBarFrom
        '
        Me.TrackBarFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBarFrom.Location = New System.Drawing.Point(12, 10)
        Me.TrackBarFrom.Name = "TrackBarFrom"
        Me.TrackBarFrom.Size = New System.Drawing.Size(472, 45)
        Me.TrackBarFrom.TabIndex = 0
        '
        'TrackBarTo
        '
        Me.TrackBarTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBarTo.Location = New System.Drawing.Point(12, 34)
        Me.TrackBarTo.Name = "TrackBarTo"
        Me.TrackBarTo.Size = New System.Drawing.Size(472, 45)
        Me.TrackBarTo.TabIndex = 1
        Me.TrackBarTo.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'LabelFchFrom
        '
        Me.LabelFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFchFrom.AutoSize = True
        Me.LabelFchFrom.Location = New System.Drawing.Point(490, 13)
        Me.LabelFchFrom.Name = "LabelFchFrom"
        Me.LabelFchFrom.Size = New System.Drawing.Size(39, 13)
        Me.LabelFchFrom.TabIndex = 2
        Me.LabelFchFrom.Text = "Label1"
        '
        'LabelFchTo
        '
        Me.LabelFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFchTo.AutoSize = True
        Me.LabelFchTo.Location = New System.Drawing.Point(490, 46)
        Me.LabelFchTo.Name = "LabelFchTo"
        Me.LabelFchTo.Size = New System.Drawing.Size(39, 13)
        Me.LabelFchTo.TabIndex = 3
        Me.LabelFchTo.Text = "Label1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 281)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(561, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_ProductStats1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(-1, 85)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(561, 304)
        Me.Panel1.TabIndex = 6
        '
        'Xl_ProductStats1
        '
        Me.Xl_ProductStats1.AllowUserToAddRows = False
        Me.Xl_ProductStats1.AllowUserToDeleteRows = False
        Me.Xl_ProductStats1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductStats1.DisplayObsolets = False
        Me.Xl_ProductStats1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductStats1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_ProductStats1.MouseIsDown = False
        Me.Xl_ProductStats1.Name = "Xl_ProductStats1"
        Me.Xl_ProductStats1.ReadOnly = True
        Me.Xl_ProductStats1.Size = New System.Drawing.Size(561, 281)
        Me.Xl_ProductStats1.TabIndex = 5
        '
        'Frm_ProductCategoryStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 391)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelFchTo)
        Me.Controls.Add(Me.LabelFchFrom)
        Me.Controls.Add(Me.TrackBarTo)
        Me.Controls.Add(Me.TrackBarFrom)
        Me.Name = "Frm_ProductCategoryStats"
        Me.Text = "Frm_ProductCategoryStats"
        CType(Me.TrackBarFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ProductStats1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TrackBarFrom As TrackBar
    Friend WithEvents TrackBarTo As TrackBar
    Friend WithEvents LabelFchFrom As Label
    Friend WithEvents LabelFchTo As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_ProductStats1 As Xl_ProductStats
End Class
