<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_GoogleMaps
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
        Me.GMapControl1 = New GMap.NET.WindowsForms.GMapControl()
        Me.PictureBoxZoomIn = New System.Windows.Forms.PictureBox()
        Me.PictureBoxZoomOut = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxZoomIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxZoomOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GMapControl1
        '
        Me.GMapControl1.Bearing = 0!
        Me.GMapControl1.CanDragMap = True
        Me.GMapControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GMapControl1.EmptyTileColor = System.Drawing.Color.Navy
        Me.GMapControl1.GrayScaleMode = False
        Me.GMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow
        Me.GMapControl1.LevelsKeepInMemmory = 5
        Me.GMapControl1.Location = New System.Drawing.Point(0, 0)
        Me.GMapControl1.MarkersEnabled = True
        Me.GMapControl1.MaxZoom = 2
        Me.GMapControl1.MinZoom = 2
        Me.GMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter
        Me.GMapControl1.Name = "GMapControl1"
        Me.GMapControl1.NegativeMode = False
        Me.GMapControl1.PolygonsEnabled = True
        Me.GMapControl1.RetryLoadTile = 0
        Me.GMapControl1.RoutesEnabled = True
        Me.GMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.[Integer]
        Me.GMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.GMapControl1.ShowTileGridLines = False
        Me.GMapControl1.Size = New System.Drawing.Size(150, 150)
        Me.GMapControl1.TabIndex = 0
        Me.GMapControl1.Zoom = 0R
        '
        'PictureBoxZoomIn
        '
        Me.PictureBoxZoomIn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxZoomIn.Image = Global.Mat.Net.My.Resources.Resources.zoom_in
        Me.PictureBoxZoomIn.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxZoomIn.Name = "PictureBoxZoomIn"
        Me.PictureBoxZoomIn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxZoomIn.TabIndex = 1
        Me.PictureBoxZoomIn.TabStop = False
        '
        'PictureBoxZoomOut
        '
        Me.PictureBoxZoomOut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxZoomOut.Image = Global.Mat.Net.My.Resources.Resources.zoom_out
        Me.PictureBoxZoomOut.Location = New System.Drawing.Point(0, 22)
        Me.PictureBoxZoomOut.Name = "PictureBoxZoomOut"
        Me.PictureBoxZoomOut.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxZoomOut.TabIndex = 2
        Me.PictureBoxZoomOut.TabStop = False
        '
        'Xl_GoogleMaps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBoxZoomOut)
        Me.Controls.Add(Me.PictureBoxZoomIn)
        Me.Controls.Add(Me.GMapControl1)
        Me.Name = "Xl_GoogleMaps"
        CType(Me.PictureBoxZoomIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxZoomOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GMapControl1 As GMap.NET.WindowsForms.GMapControl
    Friend WithEvents PictureBoxZoomIn As PictureBox
    Friend WithEvents PictureBoxZoomOut As PictureBox
End Class
