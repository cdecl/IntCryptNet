import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.security.InvalidAlgorithmParameterException;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

import sun.misc.BASE64Decoder;
import sun.misc.BASE64Encoder;


public class AES {
	public static void main(String[] args) {
		try{
			int[] arrkey = {0x60,0x8f,0x20,0xd1,0xca,0x8b,0x98,0xf0,0x6a,0x0c,0x0d,0x1a,0x82,0x7a,0xf4,0x92};
			int[] arriv = {0x0e,0xc3,0xfc,0xe5,0xae,0x43,0x00,0x3d,0x92,0x15,0x9c,0x69,0x4c,0x53,0x11,0x58};

			byte[] key = new byte[16];
			byte[] iv = new byte[16];

			for (int i = 0; i < 16; ++i) {
				key[i] = (byte)arrkey[i];
				iv[i] = (byte)arriv[i];
			}


			if (args.length != 1)
			{
				System.out.println("ASE [암호화문자열]");
				return;
			}

			String text = args[0];
			String encrypted = encrypt(text, iv, key);
			System.out.println("Java");
			System.out.println(encrypted );
			String decrypted = decrypt(encrypted,iv,key);
			System.out.println(decrypted );

		}
		catch (Exception e){
			e.printStackTrace();
		}
	}

	public static String encrypt(String text, byte[] iv, byte[] key) throws Exception{
		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");

		SecretKeySpec keySpec = new SecretKeySpec(key, "AES");
		IvParameterSpec ivSpec = new IvParameterSpec(iv);

		cipher.init(Cipher.ENCRYPT_MODE,keySpec,ivSpec);
		byte [] results = cipher.doFinal(text.getBytes("euc-kr"));
		BASE64Encoder encoder = new BASE64Encoder();
		return encoder.encode(results);
	}

	public static String decrypt(String text, byte[] iv, byte[] key) throws Exception{
		Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");

		SecretKeySpec keySpec = new SecretKeySpec(key, "AES");
		IvParameterSpec ivSpec = new IvParameterSpec(iv);
		cipher.init(Cipher.DECRYPT_MODE,keySpec,ivSpec);

		BASE64Decoder decoder = new BASE64Decoder();
		byte [] results = cipher.doFinal(decoder.decodeBuffer(text));
		return new String(results,"euc-kr");

	}

}