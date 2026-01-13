<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_SurveyTree
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
        Me.Xl_QuizTree1 = New Winforms.Xl_SurveyTree()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearchParticipants = New Winforms.Xl_TextboxSearch()
        Me.Xl_SurveyParticipants1 = New Winforms.Xl_SurveyParticipants()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelParticipacio = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelValoracio = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_SurveyParticipants1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_QuizTree1
        '
        Me.Xl_QuizTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_QuizTree1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_QuizTree1.Name = "Xl_QuizTree1"
        Me.Xl_QuizTree1.Size = New System.Drawing.Size(470, 346)
        Me.Xl_QuizTree1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 38)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(484, 378)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_QuizTree1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(476, 352)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "diseny"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearchParticipants)
        Me.TabPage2.Controls.Add(Me.Xl_SurveyParticipants1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(476, 333)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "participants"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearchParticipants
        '
        Me.Xl_TextboxSearchParticipants.Location = New System.Drawing.Point(263, 10)
        Me.Xl_TextboxSearchParticipants.Name = "Xl_TextboxSearchParticipants"
        Me.Xl_TextboxSearchParticipants.Size = New System.Drawing.Size(210, 20)
        Me.Xl_TextboxSearchParticipants.TabIndex = 1
        '
        'Xl_SurveyParticipants1
        '
        Me.Xl_SurveyParticipants1.AllowUserToAddRows = False
        Me.Xl_SurveyParticipants1.AllowUserToDeleteRows = False
        Me.Xl_SurveyParticipants1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_SurveyParticipants1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SurveyParticipants1.DisplayObsolets = False
        Me.Xl_SurveyParticipants1.Filter = Nothing
        Me.Xl_SurveyParticipants1.Location = New System.Drawing.Point(0, 36)
        Me.Xl_SurveyParticipants1.Name = "Xl_SurveyParticipants1"
        Me.Xl_SurveyParticipants1.ReadOnly = True
        Me.Xl_SurveyParticipants1.Size = New System.Drawing.Size(476, 340)
        Me.Xl_SurveyParticipants1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSplitButton1, Me.ToolStripStatusLabelParticipacio, Me.ToolStripStatusLabelValoracio})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 419)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(484, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelParticipacio
        '
        Me.ToolStripStatusLabelParticipacio.Name = "ToolStripStatusLabelParticipacio"
        Me.ToolStripStatusLabelParticipacio.Size = New System.Drawing.Size(176, 17)
        Me.ToolStripStatusLabelParticipacio.Text = "ToolStripStatusLabelParticipants"
        '
        'ToolStripStatusLabelValoracio
        '
        Me.ToolStripStatusLabelValoracio.Name = "ToolStripStatusLabelValoracio"
        Me.ToolStripStatusLabelValoracio.Size = New System.Drawing.Size(162, 17)
        Me.ToolStripStatusLabelValoracio.Text = "ToolStripStatusLabelValoracio"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton1.Image = Global.Winforms.My.Resources.Resources.refresca
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(32, 20)
        Me.ToolStripSplitButton1.Text = "ToolStripSplitButtonRefresh"
        Me.ToolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.ToolStripSplitButton1.ToolTipText = "refresca"
        '
        'Frm_SurveyTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 441)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_SurveyTree"
        Me.Text = "Enquesta"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_SurveyParticipants1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_QuizTree1 As Xl_SurveyTree
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_TextboxSearchParticipants As Xl_TextboxSearch
    Friend WithEvents Xl_SurveyParticipants1 As Xl_SurveyParticipants
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripSplitButton1 As ToolStripSplitButton
    Friend WithEvents ToolStripStatusLabelParticipacio As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelValoracio As ToolStripStatusLabel
End Class
