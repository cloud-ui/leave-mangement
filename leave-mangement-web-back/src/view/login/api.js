import {server} from '@/tools/servers'

/**
 * 登录
 */
export class LoginApi {
  // 登录
  static login (data) {
    console.log(data);
    return server.connection('POST', 'api/BackUser/Login', data)
  }
}