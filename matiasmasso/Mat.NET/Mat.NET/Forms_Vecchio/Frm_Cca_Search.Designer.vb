<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Cca_Search
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Cca_Search))
        Me.PictureBoxNotFound = New System.Windows.Forms.PictureBox
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_AmtCur1 = New Xl_AmountCur
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ButtonSearch = New System.Windows.Forms.Button
        CType(Me.PictureBoxNotFound, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxNotFound
        '
        Me.PictureBoxNotFound.Image = CType(resources.GetObject("PictureBoxNotFound.Image"), System.Drawing.Image)
        Me.PictureBoxNotFound.Location = New System.Drawing.Point(221, 76)
        Me.PictureBoxNotFound.Name = "PictureBoxNotFound"
        Me.PictureBoxNotFound.Size = New System.Drawing.Size(100, 96)
        Me.PictureBoxNotFound.TabIndex = 9
        Me.PictureBoxNotFound.TabStop = False
        Me.PictureBoxNotFound.Visible = False
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(61, 4)
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(64, 20)
        Me.NumericUpDownYea.TabIndex = 1
        Me.NumericUpDownYea.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(5, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "exercisi:"
        '
        'Xl_AmtCur1
        '
        Me.Xl_AmtCur1.Amt = Nothing
        Me.Xl_AmtCur1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmtCur1.Location = New System.Drawing.Point(364, 4)
        Me.Xl_AmtCur1.Name = "Xl_AmtCur1"
        Me.Xl_AmtCur1.Size = New System.Drawing.Size(168, 20)
        Me.Xl_AmtCur1.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(8, 30)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(605, 232)
        Me.DataGridView1.TabIndex = 4
        Me.DataGridView1.TabStop = False
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Image = My.Resources.Resources.search_16
        Me.ButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSearch.Location = New System.Drawing.Point(538, 4)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSearch.TabIndex = 3
        Me.ButtonSearch.Text = "busca..."
        Me.ButtonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'Frm_Cca_Search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 266)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.PictureBoxNotFound)
        Me.Controls.Add(Me.NumericUpDownYea)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_AmtCur1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Frm_Cca_Search"
        Me.Text = "BUSCAR ASSENTAMENTS"
        CType(Me.PictureBoxNotFound, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBoxNotFound As System.Windows.Forms.PictureBox
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCur1 As Xl_AmountCur
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
End Class
