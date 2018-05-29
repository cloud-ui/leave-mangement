import {server} from '@/tools/servers'

/**
 * 登录
 */
export class AddCompanyApi {
  // 登录
  static getAuthCode(data) {
    console.log(data);
    return server.connection('POST', 'api/BackUser/Login', data)
  }
}