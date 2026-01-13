

Public Class Xl_Contact_Mail
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Xl_Adr1 As Xl_Adr

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Xl_Adr1 = New Xl_Adr
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Gold
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = My.Resources.Resources.correos2
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(356, 54)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Xl_Adr1
        '
        Me.Xl_Adr1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Xl_Adr1.Location = New System.Drawing.Point(0, 54)
        Me.Xl_Adr1.Name = "Xl_Adr1"
        Me.Xl_Adr1.Size = New System.Drawing.Size(356, 41)
        Me.Xl_Adr1.TabIndex = 1
        '
        'Xl_Contact_Mail
        '
        Me.Controls.Add(Me.Xl_Adr1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Xl_Contact_Mail"
        Me.Size = New System.Drawing.Size(356, 94)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Event AfterUpdate()

    Public Property Adr() As Adr
        Get
            Return Xl_Adr1.Adr
        End Get
        Set(ByVal value As Adr)
            If value IsNot Nothing Then
                Xl_Adr1.Adr = value
            End If
        End Set
    End Property

    Private Sub Xl_Adr1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Adr1.Changed
        RaiseEvent AfterUpdate()
    End Sub
End Class
