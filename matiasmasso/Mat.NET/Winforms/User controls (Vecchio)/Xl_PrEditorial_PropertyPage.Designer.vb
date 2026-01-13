<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_PrEditorial_PropertyPage
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DataGridViewRevistes = New System.Windows.Forms.DataGridView
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Xl_PrOrdresDeCompra1 = New Xl_PrOrdresDeCompra
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Xl_PrInsercions1 = New Xl_PrInsercions
        Me.Xl_Contact_Logo1 = New Xl_Contact_Logo
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridViewRevistes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(3, 57)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(344, 340)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(336, 314)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(16, 26)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.ReadOnly = True
        Me.TextBoxNom.Size = New System.Drawing.Size(299, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridViewRevistes)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(336, 314)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Publicacions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridViewRevistes
        '
        Me.DataGridViewRevistes.AllowUserToAddRows = False
        Me.DataGridViewRevistes.AllowUserToDeleteRows = False
        Me.DataGridViewRevistes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewRevistes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewRevistes.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewRevistes.Name = "DataGridViewRevistes"
        Me.DataGridViewRevistes.ReadOnly = True
        Me.DataGridViewRevistes.Size = New System.Drawing.Size(330, 308)
        Me.DataGridViewRevistes.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_PrOrdresDeCompra1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(336, 314)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Ordres de compra"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_PrOrdresDeCompra1
        '
        Me.Xl_PrOrdresDeCompra1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrOrdresDeCompra1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_PrOrdresDeCompra1.Name = "Xl_PrOrdresDeCompra1"
        Me.Xl_PrOrdresDeCompra1.Size = New System.Drawing.Size(330, 308)
        Me.Xl_PrOrdresDeCompra1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_PrInsercions1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(336, 314)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "insercions"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_PrInsercions1
        '
        Me.Xl_PrInsercions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PrInsercions1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PrInsercions1.Name = "Xl_PrInsercions1"
        Me.Xl_PrInsercions1.Size = New System.Drawing.Size(336, 314)
        Me.Xl_PrInsercions1.TabIndex = 0
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(193, 3)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 2
        '
        'Xl_PrEditorial_PropertyPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Xl_PrEditorial_PropertyPage"
        Me.Size = New System.Drawing.Size(350, 400)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridViewRevistes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewRevistes As System.Windows.Forms.DataGridView
    Friend WithEvents Xl_PrOrdresDeCompra1 As Xl_PrOrdresDeCompra
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Xl_PrInsercions1 As Xl_PrInsercions
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo

End Class
