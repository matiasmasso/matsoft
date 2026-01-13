<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_StreetView
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
        Me.PictureBoxView = New System.Windows.Forms.PictureBox()
        Me.PictureBoxZoomIn = New System.Windows.Forms.PictureBox()
        Me.PictureBoxZoomOut = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxZoomIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxZoomOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxView
        '
        Me.PictureBoxView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxView.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxView.Name = "PictureBoxView"
        Me.PictureBoxView.Size = New System.Drawing.Size(173, 122)
        Me.PictureBoxView.TabIndex = 0
        Me.PictureBoxView.TabStop = False
        '
        'PictureBoxZoomIn
        '
        Me.PictureBoxZoomIn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxZoomIn.Image = Global.Winforms.My.Resources.Resources.zoom_in
        Me.PictureBoxZoomIn.Location = New System.Drawing.Point(3, 3)
        Me.PictureBoxZoomIn.Name = "PictureBoxZoomIn"
        Me.PictureBoxZoomIn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxZoomIn.TabIndex = 1
        Me.PictureBoxZoomIn.TabStop = False
        '
        'PictureBoxZoomOut
        '
        Me.PictureBoxZoomOut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxZoomOut.Image = Global.Winforms.My.Resources.Resources.zoom_out
        Me.PictureBoxZoomOut.Location = New System.Drawing.Point(3, 25)
        Me.PictureBoxZoomOut.Name = "PictureBoxZoomOut"
        Me.PictureBoxZoomOut.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxZoomOut.TabIndex = 2
        Me.PictureBoxZoomOut.TabStop = False
        '
        'Xl_StreetView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PictureBoxZoomOut)
        Me.Controls.Add(Me.PictureBoxZoomIn)
        Me.Controls.Add(Me.PictureBoxView)
        Me.Name = "Xl_StreetView"
        Me.Size = New System.Drawing.Size(173, 122)
        CType(Me.PictureBoxView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxZoomIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxZoomOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBoxView As PictureBox
    Friend WithEvents PictureBoxZoomIn As PictureBox
    Friend WithEvents PictureBoxZoomOut As PictureBox
End Class
