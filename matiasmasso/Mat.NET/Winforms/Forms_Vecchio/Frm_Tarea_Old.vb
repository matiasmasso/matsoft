Public Class Frm_Tarea_Old
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PictureBoxWait As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxEnd As System.Windows.Forms.PictureBox
    Friend WithEvents LabelText As System.Windows.Forms.Label
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBoxWait = New System.Windows.Forms.PictureBox
        Me.PictureBoxEnd = New System.Windows.Forms.PictureBox
        Me.LabelText = New System.Windows.Forms.Label
        Me.ButtonExit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PictureBoxWait
        '
        Me.PictureBoxWait.Location = New System.Drawing.Point(120, 8)
        Me.PictureBoxWait.Name = "PictureBoxWait"
        Me.PictureBoxWait.Size = New System.Drawing.Size(40, 32)
        Me.PictureBoxWait.TabIndex = 7
        Me.PictureBoxWait.TabStop = False
        '
        'PictureBoxEnd
        '
        Me.PictureBoxEnd.Location = New System.Drawing.Point(128, 80)
        Me.PictureBoxEnd.Name = "PictureBoxEnd"
        Me.PictureBoxEnd.Size = New System.Drawing.Size(24, 24)
        Me.PictureBoxEnd.TabIndex = 6
        Me.PictureBoxEnd.TabStop = False
        Me.PictureBoxEnd.Visible = False
        '
        'LabelText
        '
        Me.LabelText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelText.Location = New System.Drawing.Point(48, 48)
        Me.LabelText.Name = "LabelText"
        Me.LabelText.Size = New System.Drawing.Size(184, 16)
        Me.LabelText.TabIndex = 5
        '
        'ButtonExit
        '
        Me.ButtonExit.Enabled = False
        Me.ButtonExit.Location = New System.Drawing.Point(192, 72)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(88, 24)
        Me.ButtonExit.TabIndex = 8
        Me.ButtonExit.Text = "SORTIDA"
        '
        'Frm_Tarea
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 110)
        Me.Controls.Add(Me.ButtonExit)
        Me.Controls.Add(Me.PictureBoxWait)
        Me.Controls.Add(Me.PictureBoxEnd)
        Me.Controls.Add(Me.LabelText)
        Me.Name = "Frm_Tarea"
        Me.Text = "EXECUTANT..."
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public WriteOnly Property Caption() As String
        Set(ByVal Value As String)
            LabelText.Text = Value
            Cursor = Cursors.WaitCursor
            System.Windows.Forms.Application.DoEvents()
        End Set
    End Property

    Public Sub Fin(Optional ByVal sCaption As String = "")
        Cursor = Cursors.Default
        LabelText.Text = sCaption
        PictureBoxWait.Visible = False
        PictureBoxEnd.Visible = True
        ButtonExit.Enabled = True
    End Sub


    Private Sub ButtonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub
End Class
