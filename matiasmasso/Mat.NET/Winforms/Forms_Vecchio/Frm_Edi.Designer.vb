<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Edi
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewInboxHeader = New System.Windows.Forms.DataGridView()
        Me.DataGridViewInboxDetail = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewOutboxHeader = New System.Windows.Forms.DataGridView()
        Me.DataGridViewOutboxDetail = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFiltro = New System.Windows.Forms.TextBox()
        Me.ButtonFilter = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewInboxHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewInboxDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewOutboxHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewOutboxDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(2, 38)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(824, 433)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(816, 407)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Bandeja de entrada"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewInboxHeader)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewInboxDetail)
        Me.SplitContainer1.Size = New System.Drawing.Size(810, 401)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 1
        '
        'DataGridViewInboxHeader
        '
        Me.DataGridViewInboxHeader.AllowUserToAddRows = False
        Me.DataGridViewInboxHeader.AllowUserToDeleteRows = False
        Me.DataGridViewInboxHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewInboxHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewInboxHeader.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewInboxHeader.Name = "DataGridViewInboxHeader"
        Me.DataGridViewInboxHeader.ReadOnly = True
        Me.DataGridViewInboxHeader.Size = New System.Drawing.Size(200, 401)
        Me.DataGridViewInboxHeader.TabIndex = 0
        '
        'DataGridViewInboxDetail
        '
        Me.DataGridViewInboxDetail.AllowUserToAddRows = False
        Me.DataGridViewInboxDetail.AllowUserToDeleteRows = False
        Me.DataGridViewInboxDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewInboxDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewInboxDetail.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewInboxDetail.Name = "DataGridViewInboxDetail"
        Me.DataGridViewInboxDetail.ReadOnly = True
        Me.DataGridViewInboxDetail.Size = New System.Drawing.Size(606, 401)
        Me.DataGridViewInboxDetail.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(816, 407)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Bandeja de sortida"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewOutboxHeader)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridViewOutboxDetail)
        Me.SplitContainer2.Size = New System.Drawing.Size(810, 401)
        Me.SplitContainer2.SplitterDistance = 200
        Me.SplitContainer2.TabIndex = 0
        '
        'DataGridViewOutboxHeader
        '
        Me.DataGridViewOutboxHeader.AllowUserToAddRows = False
        Me.DataGridViewOutboxHeader.AllowUserToDeleteRows = False
        Me.DataGridViewOutboxHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewOutboxHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewOutboxHeader.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewOutboxHeader.Name = "DataGridViewOutboxHeader"
        Me.DataGridViewOutboxHeader.ReadOnly = True
        Me.DataGridViewOutboxHeader.Size = New System.Drawing.Size(200, 401)
        Me.DataGridViewOutboxHeader.TabIndex = 1
        '
        'DataGridViewOutboxDetail
        '
        Me.DataGridViewOutboxDetail.AllowUserToAddRows = False
        Me.DataGridViewOutboxDetail.AllowUserToDeleteRows = False
        Me.DataGridViewOutboxDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewOutboxDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewOutboxDetail.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewOutboxDetail.Name = "DataGridViewOutboxDetail"
        Me.DataGridViewOutboxDetail.ReadOnly = True
        Me.DataGridViewOutboxDetail.Size = New System.Drawing.Size(606, 401)
        Me.DataGridViewOutboxDetail.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(488, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "filtro"
        '
        'TextBoxFiltro
        '
        Me.TextBoxFiltro.Location = New System.Drawing.Point(533, 12)
        Me.TextBoxFiltro.Name = "TextBoxFiltro"
        Me.TextBoxFiltro.Size = New System.Drawing.Size(255, 20)
        Me.TextBoxFiltro.TabIndex = 2
        '
        'ButtonFilter
        '
        Me.ButtonFilter.Enabled = False
        Me.ButtonFilter.Location = New System.Drawing.Point(786, 11)
        Me.ButtonFilter.Name = "ButtonFilter"
        Me.ButtonFilter.Size = New System.Drawing.Size(36, 23)
        Me.ButtonFilter.TabIndex = 3
        Me.ButtonFilter.Text = "..."
        Me.ButtonFilter.UseVisualStyleBackColor = True
        '
        'Frm_Edi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 474)
        Me.Controls.Add(Me.ButtonFilter)
        Me.Controls.Add(Me.TextBoxFiltro)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Edi"
        Me.Text = "EDI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewInboxHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewInboxDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewOutboxHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewOutboxDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewInboxHeader As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewInboxDetail As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewOutboxHeader As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewOutboxDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFiltro As System.Windows.Forms.TextBox
    Friend WithEvents ButtonFilter As System.Windows.Forms.Button
End Class
