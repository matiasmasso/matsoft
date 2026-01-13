Public Class Frm_MiscImg
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
    Friend WithEvents TextBoxId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Image1 As Xl_Image
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBoxId = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_Image1 = New Xl_Image
        Me.SuspendLayout()
        '
        'TextBoxId
        '
        Me.TextBoxId.Location = New System.Drawing.Point(72, 16)
        Me.TextBoxId.Name = "TextBoxId"
        Me.TextBoxId.Size = New System.Drawing.Size(192, 20)
        Me.TextBoxId.TabIndex = 0
        Me.TextBoxId.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Id:"
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.Location = New System.Drawing.Point(72, 40)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(192, 192)
        Me.Xl_Image1.TabIndex = 4
        '
        'Frm_MiscImg
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 270)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxId)
        Me.Name = "Frm_MiscImg"
        Me.Text = "IMATGES MISCELANEA"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private mImg As System.Drawing.Image

   

    Private Sub TextBoxId_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBoxId.Validating
        Dim oMisc As New maxisrvr.MiscImg(TextBoxId.Text)
        Xl_Image1.Bitmap = oMisc.Image
    End Sub

    Private Sub Xl_Image1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        Dim oMisc As New MaxiSrvr.MiscImg(TextBoxId.Text)
        oMisc.Image = Xl_Image1.Bitmap
        oMisc.Update()
    End Sub
End Class
