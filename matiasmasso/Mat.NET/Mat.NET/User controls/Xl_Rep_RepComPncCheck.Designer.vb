<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Rep_RepComPncCheck
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
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.PanelBottomBar = New System.Windows.Forms.Panel()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonSyncRps = New System.Windows.Forms.Button()
        Me.ButtonUpdateArc = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PanelBottomBar.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonStart
        '
        Me.ButtonStart.Location = New System.Drawing.Point(0, 0)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(99, 23)
        Me.ButtonStart.TabIndex = 2
        Me.ButtonStart.Text = "Carregar dades"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'PanelBottomBar
        '
        Me.PanelBottomBar.Controls.Add(Me.LabelCount)
        Me.PanelBottomBar.Controls.Add(Me.ProgressBar1)
        Me.PanelBottomBar.Controls.Add(Me.ButtonCancel)
        Me.PanelBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottomBar.Location = New System.Drawing.Point(0, 249)
        Me.PanelBottomBar.Name = "PanelBottomBar"
        Me.PanelBottomBar.Size = New System.Drawing.Size(457, 23)
        Me.PanelBottomBar.TabIndex = 6
        Me.PanelBottomBar.Visible = False
        '
        'LabelCount
        '
        Me.LabelCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCount.AutoSize = True
        Me.LabelCount.Location = New System.Drawing.Point(362, 5)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(0, 13)
        Me.LabelCount.TabIndex = 6
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(356, 23)
        Me.ProgressBar1.TabIndex = 5
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.ButtonCancel.Location = New System.Drawing.Point(389, 0)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(68, 23)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "Cancelar"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ButtonSyncRps)
        Me.Panel1.Controls.Add(Me.ButtonUpdateArc)
        Me.Panel1.Controls.Add(Me.ButtonStart)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(457, 23)
        Me.Panel1.TabIndex = 7
        '
        'ButtonSyncRps
        '
        Me.ButtonSyncRps.Location = New System.Drawing.Point(228, 0)
        Me.ButtonSyncRps.Name = "ButtonSyncRps"
        Me.ButtonSyncRps.Size = New System.Drawing.Size(164, 23)
        Me.ButtonSyncRps.TabIndex = 7
        Me.ButtonSyncRps.Text = "actualitzar pendents de liquidar"
        Me.ButtonSyncRps.UseVisualStyleBackColor = True
        '
        'ButtonUpdateArc
        '
        Me.ButtonUpdateArc.Location = New System.Drawing.Point(105, 0)
        Me.ButtonUpdateArc.Name = "ButtonUpdateArc"
        Me.ButtonUpdateArc.Size = New System.Drawing.Size(117, 23)
        Me.ButtonUpdateArc.TabIndex = 6
        Me.ButtonUpdateArc.Text = "actualitzar sortides"
        Me.ButtonUpdateArc.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 29)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(457, 222)
        Me.DataGridView1.TabIndex = 8
        '
        'Xl_Rep_RepComPncCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelBottomBar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Xl_Rep_RepComPncCheck"
        Me.Size = New System.Drawing.Size(457, 272)
        Me.PanelBottomBar.ResumeLayout(False)
        Me.PanelBottomBar.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonStart As System.Windows.Forms.Button
    Friend WithEvents PanelBottomBar As System.Windows.Forms.Panel
    Friend WithEvents LabelCount As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonSyncRps As System.Windows.Forms.Button
    Friend WithEvents ButtonUpdateArc As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView

End Class
