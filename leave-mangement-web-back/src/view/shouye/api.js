import {server} from '@/tools/servers'

/**
 * 公告
 */
export class ShouyeApi {
  //获取列表
  static noticeList(){
      return server.connection('GET','api/Notice/GetNoticeList')
  }
  static getAttendanceData(){
    return server.connection('GET','api/Attendance/GetAttendanceData')
  }
  // 获取公司信息
  static getCompany () {
    return server.connection('GET', 'api/File/GetCompany')
  }
}