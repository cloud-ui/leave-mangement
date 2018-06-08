import {server} from '@/tools/servers'

/**
 * 登录
 */
export class FileApi {
  // 获取公司信息
  static getCompany () {
    return server.connection('GET', 'api/BackFile/GetCompanyInfo')
  }
  static getDeparmentList(data={}){
    return server.connection('POST','api/BackFile/GetDeparmentList',data)
  }
}