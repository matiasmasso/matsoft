Public Class ElCorteInglesDept

    Shared Function Find(oGuid As Guid) As DTO.Integracions.ElCorteIngles.Dept
        Return ElCorteInglesDeptLoader.Find(oGuid)
    End Function

    Shared Function Update(oTemplate As DTO.Integracions.ElCorteIngles.Dept, exs As List(Of Exception)) As Boolean
        Return ElCorteInglesDeptLoader.Update(oTemplate, exs)
    End Function

    Shared Function Delete(oTemplate As DTO.Integracions.ElCorteIngles.Dept, exs As List(Of Exception)) As Boolean
        Return ElCorteInglesDeptLoader.Delete(oTemplate, exs)
    End Function

End Class


Public Class ElCorteInglesDepts
    Shared Function All() As List(Of DTO.Integracions.ElCorteIngles.Dept)
        Return ElCorteInglesDeptsLoader.All()
    End Function

    Shared Function AlineamientoDisponibilidad() As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
        Return AlineamientoDeDisponibilidadLoader.Factory()
    End Function


End Class

