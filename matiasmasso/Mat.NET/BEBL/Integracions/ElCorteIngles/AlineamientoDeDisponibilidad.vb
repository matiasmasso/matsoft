Namespace Integracions.ElCorteIngles


    Public Class AlineamientoDeDisponibilidad

        Shared Function Factory() As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
            Return AlineamientoDeDisponibilidadLoader.Factory()
        End Function

        Shared Function Procesa(oEmp As DTOEmp, oTaskLog As DTOTaskLog) As DTOTaskLog
            Dim retval As DTOTaskLog = oTaskLog
            Dim exs As New List(Of Exception)
            Dim value = Factory()
            SendAlineamientoDisponibilidadViaFtp(exs, value)
            If exs.Count = 0 Then
                If value.Items.Count = 0 Then
                    retval.SetResult(DTOTask.ResultCods.Empty, "no hi ha cataleg en actiu", exs)
                Else
                    Dim sb As New Text.StringBuilder()
                    Dim deptIds = value.Items.GroupBy(Function(x) x.Uneco).Select(Function(y) y.First()).Select(Function(z) z.Uneco).ToList()
                    For Each deptId In deptIds
                        Dim lins = value.Items.Where(Function(x) x.Uneco = deptId).ToList()
                        Dim disponibles = lins.Where(Function(x) x.Descatalogado() = "N" And x.Disponible() = "S").Count
                        Dim descatalogats = lins.Where(Function(x) x.Descatalogado() = "S").Count()
                        Dim rotures = lins.Where(Function(x) x.Disponible() = "N").Count()
                        Dim msg = String.Format("Dept.{0}: disponibles: {1}, descatalogats: {2}, rotures: {3}", deptId, disponibles, descatalogats, rotures)
                        sb.AppendLine(msg)
                    Next
                    retval.SetResult(DTOTask.ResultCods.Success, sb.ToString)
                End If
            Else
                retval.SetResult(DTOTask.ResultCods.Failed, "error al enviar per Ftp", exs)
            End If

            Return retval
        End Function

        Shared Function SendAlineamientoDisponibilidadViaFtp(exs As List(Of Exception), value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad) As Boolean
            Dim oByteArray = System.Text.Encoding.UTF8.GetBytes(value.Text)
            Dim filename = "STOCK.TXT"
            Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
            If BEBL.Ftpserver.Send(exs, oCustomer, DTOFtpserver.Path.Cods.Inbox, oByteArray, filename) Then
                AlineamientoDeDisponibilidadLoader.Insert(exs, value)
            End If
            Return exs.Count = 0
        End Function

        Shared Function Find(oGuid As Guid) As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
            Return AlineamientoDeDisponibilidadLoader.Find(oGuid)
        End Function

    End Class

    Public Class AlineamientosDeDisponibilidad
        Shared Function All() As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
            Return AlineamientosDeDisponibilidadLoader.All()
        End Function
    End Class
End Namespace
