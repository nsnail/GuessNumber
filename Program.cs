//猜数字输入提醒 or 失败提醒
System.Console.WriteLine(
	
	//借助Array.ForEach内置方法代替for循环
	new System.Action(() => System.Array.ForEach(
		
		// args是 main函数程序入口主参， C#9.0使用语法糖隐蔽了main函数，但此参数还在，程序的核心是利用此参数赋值语句的返回值副作用来避免开新行
		// args[0] "12345" 为主循环表，表示有5次猜数字机会
		// args[1] new Random() 随机产生一个1-100的数字
		// args[2] 用来存储用户输入的数字
		// args[3] 用来存储判断结果：如 “答对了”，“太大”，“太小”
		(args = new[] {"12345", new System.Random().Next(1, 101).ToString(), null, null})[0].ToCharArray(),
		
		//arg是args[0]的当前循环索引
		arg => new System.Action<int?>(x =>
				//x是被Invoke调用端传进来的 目标随机数，用来和Console.ReadLine接收的用户输入最比对，在打印结果的同时，
				//把“太大” “太小” “答对了” 这样的字符串结果存入到args[3]里面
				System.Console.WriteLine(args[3] = $"{((args[2] = System.Console.ReadLine()) == x.ToString()  ? "恭喜，答对了！" : int.Parse($"0{new System.Text.RegularExpressions.Regex(@"\d+").Match(args[2]).Value}") < x ? "太小了" : "太大了")}")
				//上面↑↑↑↑↑使用正则抹平用户错误输入，以免程序抛出异常
				
				
				) .Invoke(
			
			
					new System.Action<string>(System.Console.Write).DynamicInvoke(
						//结果长度大于3，就是那句“恭喜答对了“ 退出程序。 否则继续下一轮猜数字
						args[3]?.Length > 3 ?  
						new System.Action(() => System.Environment.Exit(0)).DynamicInvoke() 
					: $"猜1-100数字, [{arg}/{args[0].Length}]: "
					
					//这里使用DynamicInvoke只是为了解决void访问类型不能再接表达式的问题。
					//as int? 恒为否，将随机数字字符串转换成数字x传入上面的Action<int?>
					) as int? ?? int.Parse(args[1]))
				
				//这里DynamicInvoke ??也只为了链接语句， 执行到这里说明次数用完了，否则在上面就会主动退出程序。
				)).DynamicInvoke() ?? $"很遗憾, 您已失败. 正确答案是 [{args[1]}]");