<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Vehicle_MarcasiModels
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
        Me.Xl_VehicleMarcas1 = New Winforms.Xl_VehicleMarcas()
        Me.Xl_VehicleModels1 = New Winforms.Xl_VehicleModels()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_VehicleMarcas1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_VehicleModels1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_VehicleMarcas1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_VehicleModels1)
        Me.SplitContainer1.Size = New System.Drawing.Size(655, 261)
        Me.SplitContainer1.SplitterDistance = 218
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_VehicleMarcas1
        '
        Me.Xl_VehicleMarcas1.AllowUserToAddRows = False
        Me.Xl_VehicleMarcas1.AllowUserToDeleteRows = False
        Me.Xl_VehicleMarcas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_VehicleMarcas1.DisplayObsolets = False
        Me.Xl_VehicleMarcas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_VehicleMarcas1.Filter = Nothing
        Me.Xl_VehicleMarcas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_VehicleMarcas1.Name = "Xl_VehicleMarcas1"
        Me.Xl_VehicleMarcas1.ReadOnly = True
        Me.Xl_VehicleMarcas1.Size = New System.Drawing.Size(218, 261)
        Me.Xl_VehicleMarcas1.TabIndex = 0
        '
        'Xl_VehicleModels1
        '
        Me.Xl_VehicleModels1.AllowUserToAddRows = False
        Me.Xl_VehicleModels1.AllowUserToDeleteRows = False
        Me.Xl_VehicleModels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_VehicleModels1.DisplayObsolets = False
        Me.Xl_VehicleModels1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_VehicleModels1.Filter = Nothing
        Me.Xl_VehicleModels1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_VehicleModels1.Name = "Xl_VehicleModels1"
        Me.Xl_VehicleModels1.ReadOnly = True
        Me.Xl_VehicleModels1.Size = New System.Drawing.Size(433, 261)
        Me.Xl_VehicleModels1.TabIndex = 0
        '
        'Frm_Vehicle_MarcasiModels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Vehicle_MarcasiModels"
        Me.Text = "Marques i models de vehicle"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_VehicleMarcas1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_VehicleModels1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_VehicleMarcas1 As Xl_VehicleMarcas
    Friend WithEvents Xl_VehicleModels1 As Xl_VehicleModels
End Class
