<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cyc
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Cyc))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBoxLink = New System.Windows.Forms.PictureBox
        Me.ButtonMarginRefresh = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxMargin = New System.Windows.Forms.TextBox
        Me.ComboBoxYeas = New System.Windows.Forms.ComboBox
        Me.DataGridViewHdr = New System.Windows.Forms.DataGridView
        Me.DataGridViewDtl = New System.Windows.Forms.DataGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewPaisos = New System.Windows.Forms.DataGridView
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxLink, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewHdr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewPaisos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(512, -4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(286, 160)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'PictureBoxLink
        '
        Me.PictureBoxLink.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLink.BackColor = System.Drawing.Color.White
        Me.PictureBoxLink.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxLink.Image = CType(resources.GetObject("PictureBoxLink.Image"), System.Drawing.Image)
        Me.PictureBoxLink.Location = New System.Drawing.Point(688, 55)
        Me.PictureBoxLink.Name = "PictureBoxLink"
        Me.PictureBoxLink.Size = New System.Drawing.Size(64, 48)
        Me.PictureBoxLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxLink.TabIndex = 15
        Me.PictureBoxLink.TabStop = False
        '
        'ButtonMarginRefresh
        '
        Me.ButtonMarginRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMarginRefresh.Enabled = False
        Me.ButtonMarginRefresh.Location = New System.Drawing.Point(720, 31)
        Me.ButtonMarginRefresh.Name = "ButtonMarginRefresh"
        Me.ButtonMarginRefresh.Size = New System.Drawing.Size(32, 20)
        Me.ButtonMarginRefresh.TabIndex = 14
        Me.ButtonMarginRefresh.Text = "..."
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(648, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "marge:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(648, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "any:"
        '
        'TextBoxMargin
        '
        Me.TextBoxMargin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMargin.Location = New System.Drawing.Point(688, 31)
        Me.TextBoxMargin.Name = "TextBoxMargin"
        Me.TextBoxMargin.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxMargin.TabIndex = 11
        Me.TextBoxMargin.TabStop = False
        '
        'ComboBoxYeas
        '
        Me.ComboBoxYeas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxYeas.Location = New System.Drawing.Point(688, 7)
        Me.ComboBoxYeas.Name = "ComboBoxYeas"
        Me.ComboBoxYeas.Size = New System.Drawing.Size(64, 21)
        Me.ComboBoxYeas.TabIndex = 10
        Me.ComboBoxYeas.TabStop = False
        '
        'DataGridViewHdr
        '
        Me.DataGridViewHdr.AllowUserToAddRows = False
        Me.DataGridViewHdr.AllowUserToDeleteRows = False
        Me.DataGridViewHdr.AllowUserToResizeRows = False
        Me.DataGridViewHdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewHdr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewHdr.Location = New System.Drawing.Point(1, 0)
        Me.DataGridViewHdr.Name = "DataGridViewHdr"
        Me.DataGridViewHdr.ReadOnly = True
        Me.DataGridViewHdr.Size = New System.Drawing.Size(515, 156)
        Me.DataGridViewHdr.TabIndex = 16
        '
        'DataGridViewDtl
        '
        Me.DataGridViewDtl.AllowUserToAddRows = False
        Me.DataGridViewDtl.AllowUserToDeleteRows = False
        Me.DataGridViewDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDtl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewDtl.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewDtl.Name = "DataGridViewDtl"
        Me.DataGridViewDtl.ReadOnly = True
        Me.DataGridViewDtl.Size = New System.Drawing.Size(751, 305)
        Me.DataGridViewDtl.TabIndex = 17
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 162)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewPaisos)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewDtl)
        Me.SplitContainer1.Size = New System.Drawing.Size(751, 441)
        Me.SplitContainer1.SplitterDistance = 132
        Me.SplitContainer1.TabIndex = 18
        '
        'DataGridViewPaisos
        '
        Me.DataGridViewPaisos.AllowUserToAddRows = False
        Me.DataGridViewPaisos.AllowUserToDeleteRows = False
        Me.DataGridViewPaisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewPaisos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewPaisos.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewPaisos.Name = "DataGridViewPaisos"
        Me.DataGridViewPaisos.ReadOnly = True
        Me.DataGridViewPaisos.Size = New System.Drawing.Size(751, 132)
        Me.DataGridViewPaisos.TabIndex = 18
        '
        'Frm_Cyc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 604)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DataGridViewHdr)
        Me.Controls.Add(Me.PictureBoxLink)
        Me.Controls.Add(Me.ButtonMarginRefresh)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxMargin)
        Me.Controls.Add(Me.ComboBoxYeas)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Frm_Cyc"
        Me.Text = "CREDITO Y CAUCION"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxLink, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewHdr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewDtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewPaisos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxLink As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonMarginRefresh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMargin As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxYeas As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewHdr As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewDtl As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewPaisos As System.Windows.Forms.DataGridView
End Class
