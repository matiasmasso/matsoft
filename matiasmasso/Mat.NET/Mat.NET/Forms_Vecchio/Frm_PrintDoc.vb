Public Class Frm_PrintDoc
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
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Xl_PrintDoc1 As Xl_PrintDoc
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.Xl_PrintDoc1 = New Xl_PrintDoc
        Me.SuspendLayout()
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(192, 144)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(96, 32)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "IMPRIMIR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(88, 144)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 32)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'Xl_PrintDoc1
        '
        Me.Xl_PrintDoc1.Location = New System.Drawing.Point(40, 16)
        Me.Xl_PrintDoc1.Name = "Xl_PrintDoc1"
        Me.Xl_PrintDoc1.Size = New System.Drawing.Size(224, 120)
        Me.Xl_PrintDoc1.TabIndex = 8
        '
        'Frm_PrintDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 182)
        Me.Controls.Add(Me.Xl_PrintDoc1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Name = "Frm_PrintDoc"
        Me.Text = "IMPRIMIR DOCUMENT"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private BlCancel As Boolean


    Public ReadOnly Property Copia() As Boolean
        Get
            Return Xl_PrintDoc1.Copia
        End Get
    End Property

    Public ReadOnly Property Original() As Boolean
        Get
            Return Xl_PrintDoc1.Original
        End Get
    End Property

    Public ReadOnly Property Preview() As Boolean
        Get
            Return Xl_PrintDoc1.Preview
        End Get
    End Property

    Public ReadOnly Property Cancel() As Boolean
        Get
            Return BlCancel
        End Get
    End Property

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        BlCancel = True
        Me.Close()
    End Sub
End Class
