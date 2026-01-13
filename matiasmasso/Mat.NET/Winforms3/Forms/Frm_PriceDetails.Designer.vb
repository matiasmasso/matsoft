<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PriceDetails
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
        Me.Xl_PriceAnalysis1 = New Xl_PriceAnalysis()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Xl_Contact21 = New Xl_Contact2()
        Me.Xl_LookupProduct1 = New Xl_LookupProduct()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.Xl_PriceAnalysis1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PriceAnalysis1
        '
        Me.Xl_PriceAnalysis1.AllowUserToAddRows = False
        Me.Xl_PriceAnalysis1.AllowUserToDeleteRows = False
        Me.Xl_PriceAnalysis1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PriceAnalysis1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PriceAnalysis1.DisplayObsolets = False
        Me.Xl_PriceAnalysis1.Location = New System.Drawing.Point(3, 97)
        Me.Xl_PriceAnalysis1.MouseIsDown = False
        Me.Xl_PriceAnalysis1.Name = "Xl_PriceAnalysis1"
        Me.Xl_PriceAnalysis1.ReadOnly = True
        Me.Xl_PriceAnalysis1.Size = New System.Drawing.Size(415, 227)
        Me.Xl_PriceAnalysis1.TabIndex = 0
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(64, 71)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 1
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(64, 18)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(342, 20)
        Me.Xl_Contact21.TabIndex = 2
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(64, 45)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.ReadOnlyLookup = False
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(342, 20)
        Me.Xl_LookupProduct1.TabIndex = 3
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Client"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Producte"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Data"
        '
        'Frm_PriceDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 323)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Xl_PriceAnalysis1)
        Me.Name = "Frm_PriceDetails"
        Me.Text = "Analisis de preu"
        CType(Me.Xl_PriceAnalysis1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_PriceAnalysis1 As Xl_PriceAnalysis
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Xl_LookupProduct1 As Xl_LookupProduct
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
