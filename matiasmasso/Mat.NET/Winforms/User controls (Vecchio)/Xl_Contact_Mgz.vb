
Imports System.Data.SqlClient

Public Class Xl_Contact_Mgz
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
    Friend WithEvents CheckBoxEan As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CheckBoxEan = New System.Windows.Forms.CheckBox
        Me.TextBoxEmail = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'CheckBoxEan
        '
        Me.CheckBoxEan.AutoSize = True
        Me.CheckBoxEan.Enabled = False
        Me.CheckBoxEan.Location = New System.Drawing.Point(6, 136)
        Me.CheckBoxEan.Name = "CheckBoxEan"
        Me.CheckBoxEan.Size = New System.Drawing.Size(149, 17)
        Me.CheckBoxEan.TabIndex = 0
        Me.CheckBoxEan.Text = "Requereix codis de barres"
        Me.CheckBoxEan.UseVisualStyleBackColor = True
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Location = New System.Drawing.Point(4, 101)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.ReadOnly = True
        Me.TextBoxEmail.Size = New System.Drawing.Size(258, 20)
        Me.TextBoxEmail.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "e-mail missatges automatitzats:"
        '
        'Xl_Contact_Mgz
        '
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.CheckBoxEan)
        Me.Name = "Xl_Contact_Mgz"
        Me.Size = New System.Drawing.Size(274, 320)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mMgz As Mgz

    Public Property Mgz() As Mgz
        Get
            Return mMgz
        End Get
        Set(ByVal value As Mgz)
            If value IsNot Nothing Then
                mMgz = value
                refresca()
            End If
        End Set
    End Property

    Private Sub refresca()
        TextBoxEmail.Text = mMgz.Email
        CheckBoxEan.Checked = mMgz.EanRequired
    End Sub
End Class
