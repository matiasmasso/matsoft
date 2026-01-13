Public Class Frm_CertificatIrpf
    Private _CertificatIrpf As DTOCertificatIrpf
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCertificatIrpf)
        MyBase.New()
        Me.InitializeComponent()
        _CertificatIrpf = value
    End Sub


End Class