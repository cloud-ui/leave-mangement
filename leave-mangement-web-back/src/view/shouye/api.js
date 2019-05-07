import {server} from '@/tools/servers'

/**
 * 公告
 */
export class ShouyeApi {
  //获取列表
  static noticeList(data={}){
      return server.connection('POST','api/Notice/NoticeList',data)
  }
  static getAttendanceData(){
    return server.connection('GET','api/Attendance/GetAttendanceData')
  }
  // 获取公司信息
  static getCompany () {
    return server.connection('GET', 'api/File/GetCompany')
  }
}