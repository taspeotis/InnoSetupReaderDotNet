#include <vector>

#include "LzmaDecode.h"

public ref class LzmaDecoderSize
{
public:

	void DoSomething(array<Byte>^ propertyBytes, array<Byte>^ streamBytes)
	{
		pin_ptr<Byte> propertyBytesPinPtr = &propertyBytes[0];
		pin_ptr<Byte> streamBytesPinPtr = &streamBytes[0];

		CLzmaDecoderState state = {};

		if (LzmaDecodeProperties(&state.Properties, propertyBytesPinPtr, propertyBytes->Length) != LZMA_RESULT_OK)
			throw gcnew System::Exception("Bad!");

		std::vector<unsigned char> dictionary;
		std::vector<CProb> probs;

		dictionary.reserve(state.Properties.DictionarySize);
		probs.reserve(LzmaGetNumProbs(&state.Properties));

		state.Dictionary = dictionary.data();
		state.Probs = probs.data();

		LzmaDecoderInit(&state);

		const size_t outSize = 512000;
		unsigned char outStream[outSize] = {};

		SizeT inProcessed = 0;
		SizeT outProcessed = 0;

		if (LzmaDecode(&state, streamBytesPinPtr, streamBytes->Length, &inProcessed, outStream, outSize, &outProcessed) != LZMA_RESULT_OK)
			throw gcnew System::Exception("Badder! " + outProcessed);
	}
};